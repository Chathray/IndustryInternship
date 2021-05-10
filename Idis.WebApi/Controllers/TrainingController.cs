using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Dynamic;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TrainingController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly IOriginService _origin;

        public TrainingController(IMapper mapper, IServiceFactory factory, IOriginService origin)
        {
            _origin = origin;
            _mapper = mapper;
            _serviceFactory = factory;
        }


        /// <summary>
        /// Get training content
        /// </summary>
        /// <remarks>Get training content given Training Id</remarks>
        /// <param name="trainingId" example="1">Training Id</param>
        /// <returns>Training data content</returns>
        [HttpGet("Content")]
        public IActionResult GetTraining(int trainingId)
        {
            var training = _serviceFactory.Training.GetOne(trainingId);

            if (training is null)
                return NotFound();
            else
                return Ok(training.TraData.JsonPrettify());
        }


        /// <summary>
        /// Update a training
        /// </summary>
        [HttpPut]
        public IActionResult UpdateTraining(TrainingModel model)
        {
            model.CreatedBy = _origin.UserId;
            var success = _serviceFactory.Training.Update(model);

            if (success)
                return Ok(model);
            else
                return StatusCode(StatusCodes.Status501NotImplemented);
        }


        /// <summary>
        /// Delete a training
        /// </summary>
        /// <param name="trainingId" example="100"></param>
        [HttpDelete]
        public IActionResult DeleteTraining(int trainingId)
        {
            var result = _serviceFactory.Training.Delete(trainingId);
            if (result)
            {
                var hasRefreshSharedTrainings = _serviceFactory.Department.RefreshSharedTrainings(trainingId);
                return Ok(new { result, hasRefreshSharedTrainings });
            }
            return new JsonResult(result);
        }


        /// <summary>
        /// Create a training
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public IActionResult InsertTraining(TrainingModel model)
        {
            model.CreatedBy = _origin.UserId;

            var result = _serviceFactory.Training.Create(model);
            return new JsonResult(result);
        }
    }
}