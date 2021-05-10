using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class InternController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly IOriginService _origin;

        public InternController(IServiceFactory factory, IMapper mapper, IOriginService origin)
        {
            _serviceFactory = factory;
            _mapper = mapper;
            _origin = origin;
        }


        /// <summary>
        /// Get a Intern
        /// </summary>
        /// <remarks>Get a Intern given Intern Id</remarks>
        /// <param name="internId" example="310"></param>
        /// <returns>Intern model with specific info</returns>
        [HttpGet]
        public IActionResult GetIntern(int internId)
        {
            var intern = _serviceFactory.Intern.GetOne(internId);
            if (intern != null)
                return Ok(intern);
            else
                return NotFound();
        }


        /// <summary>
        /// Create or update a Intern
        /// </summary>
        /// <remarks>Create new if request have internId, else update intern info given internId</remarks>
        /// <param name="request">Model of a intern</param>
        /// <returns>True if delete success, else False</returns>
        [HttpPut]
        public IActionResult CreateIntern(CreateInternRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            var intern = _mapper.Map<InternModel>(request);
            intern.MentorId = _origin.UserId;

            if (intern.Avatar == "")
                intern.Avatar = "_intern.jpg";

            try
            {
                if (request.InternId is null)
                {
                    var created = _serviceFactory.Intern.Create(intern);
                    if (created)
                        return Ok(new { created });
                }
                else
                {
                    var updated = _serviceFactory.Intern.Update(intern);
                    if (updated)
                        return Ok(new { updated });
                }
            }
            catch (Exception Ex)
            {
                Log.Error(Ex, $"Func: {nameof(CreateIntern)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status501NotImplemented);
        }


        /// <summary>
        /// Delete a Intern
        /// </summary>
        /// <remarks>Delete a Intern given Intern Id</remarks>
        /// <param name="internId" example="311"></param>
        /// <returns>True if delete success, else False</returns>
        [HttpDelete]
        public IActionResult DeleteIntern(int internId)
        {
            var deleted = _serviceFactory.Intern.Delete(internId);
            return new JsonResult(new { internId, deleted });
        }


        /// <summary>
        /// Get intern info
        /// </summary>
        /// <remarks>Get intern info given internId</remarks>
        /// <param name="internId" example="310">Id of a intern</param>
        /// <returns>Intern info</returns>
        [HttpGet("InternInfo")]
        public IActionResult GetInternInfo(int internId)
        {
            string info = _serviceFactory.Intern.GetInternInfo(internId);
            if (info is null)
                return NotFound();
            else
                return Content(info.JsonPrettify());
        }


        /// <summary>
        /// Get intern detail info
        /// </summary>
        /// <remarks>Get intern detail info given internId</remarks>
        /// <param name="internId" example="310">Id of a intern</param>
        /// <returns>Intern detail</returns>
        [HttpGet("InternDetail")]
        public IActionResult GetInternDetail(int internId)
        {
            string detail = _serviceFactory.Intern.GetInternDetail(internId);
            if (detail is null)
                return NotFound();
            else
                return Content(detail.JsonPrettify());
        }


        /// <summary>
        /// Get passed count
        /// </summary>
        /// <remarks>Get number of intern passed an internship</remarks>
        /// <returns>Number of passed intern</returns>
        [HttpGet("PassedCount")]
        public IActionResult GetPassedCount()
        {
            var passed = _serviceFactory.Point.GetPassedCount();
            var total = _serviceFactory.User.CountByIndex(6);
            var percent = new
            {
                percent = (passed / (float)total).ToString("0%")
            };
            return Ok(percent);
        }


        /// <summary>
        /// Evaluate a intern
        /// </summary>
        /// <param name="model">Point model to evaluate</param>
        /// <returns>Done or fail</returns>
        [HttpPost]
        public IActionResult EvaluateIntern(EvaluateRequest model)
        {
            var point = _mapper.Map<PointModel>(model);
            point.MarkerId = _origin.UserId;
            var success = _serviceFactory.Point.EvaluateIntern(point);

            if (success)
                return Ok();
            else
                return StatusCode(StatusCodes.Status501NotImplemented);
        }


        /// <summary>
        /// Get Training list of a intern
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <param name="internId" example="310">The intern id</param>
        /// <response code="200">List has been retrieved</response>
        /// <response code="400">Bad request, please try later</response>
        [ProducesResponseType(typeof(IList<TrainingModel>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [Produces("application/json")]
        [HttpGet("JointTrainings")]
        public IList<TrainingModel> GetJointTrainings(int internId)
        {
            var list = _serviceFactory.Intern.GetJointTrainings(internId);
            return list;
        }
    }
}