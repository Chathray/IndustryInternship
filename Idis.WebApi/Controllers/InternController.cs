using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Dynamic;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/Intern")]
    public class InternController : ControllerBase
    {
        private readonly ILogger<InternController> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public InternController(ILogger<InternController> logger, IMapper mapper, IServiceFactory factory)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceFactory = factory;
        }


        #region GET
        /// <summary>
        /// Get Training list of a intern
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <param name="internId" example="7">The intern id</param>
        /// <response code="200">List has been retrieved</response>
        /// <response code="400">Bad request, please try later</response>
        [ProducesResponseType(typeof(IList<TrainingModel>), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [Produces("application/json")]
        [HttpGet("GetJointTrainings")]
        public IList<TrainingModel> GetJointTrainings(int internId)
        {
            var obj = _serviceFactory.Intern.GetJointTrainings(internId);
            return obj;
        }


        /// <summary>
        /// Count rows in a table/schema
        /// </summary>
        /// <remarks>Count rows in a table/schema given table/schema Id</remarks>
        /// <param name="index">Id of schema to count</param>
        /// <returns>Number of rows given table index</returns>
        [HttpGet("CountByIndex/{index}")]
        public int CountByIndex(int index)
        {
            return _serviceFactory.User.CountByIndex(index);
        }


        /// <summary>
        /// Get intern info
        /// </summary>
        /// <remarks>Get intern info given internId</remarks>
        /// <param name="id">Id of a intern</param>
        /// <returns>Intern info</returns>
        [HttpGet("Home/GetInternInfo")]
        [HttpGet("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            var json = _serviceFactory.Intern.GetInternInfo(id);
            return json;
        }


        /// <summary>
        /// Get intern detail info
        /// </summary>
        /// <remarks>Get intern detail info given internId</remarks>
        /// <param name="id">Id of a intern</param>
        /// <returns>Intern detail</returns>
        [HttpGet("GetInternDetail")]
        public string GetInternDetail(int id)
        {
            var json = _serviceFactory.Intern.GetInternDetail(id);
            return json;
        }


        /// <summary>
        /// Get training content
        /// </summary>
        /// <remarks>Get training content given Training Id</remarks>
        /// <param name="id">Training Id</param>
        /// <returns>Training data content</returns>
        [HttpGet("GetTrainingContent")]
        public string GetTrainingContent(int id)
        {
            return _serviceFactory.Training.GetOne(id).TraData;
        }


        /// <summary>
        /// Get passed count
        /// </summary>
        /// <remarks>Get number of intern passed an internship</remarks>
        /// <returns>Number of passed intern</returns>
        [HttpGet("GetPassedCount")]
        public string GetPassedCount()
        {
            var passed = _serviceFactory.Point.GetPassedCount();
            var total = CountByIndex(6);
            return (passed / (float)total).ToString("0%");
        }
        #endregion


        /// <summary>
        /// Delete a Intern
        /// </summary>
        /// <remarks>Delete a Intern given Intern Id</remarks>
        /// <param name="id"></param>
        /// <returns>True if delete success, else False</returns>
        [HttpDelete("DeleteIntern")]
        public bool DeleteIntern(int id)
        {
            return _serviceFactory.Intern.Delete(id);
        }
    }
}