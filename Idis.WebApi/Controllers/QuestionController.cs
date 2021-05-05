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
    [Route("api/v{version:apiVersion}/intern")]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<InternController> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public QuestionController(ILogger<InternController> logger, IMapper mapper, IServiceFactory factory)
        {
            _logger = logger;
            _mapper = mapper;
            _serviceFactory = factory;
        }


        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">Question Id</param>
        /// <returns>True or False</returns>
        [HttpPost("DeleteQuestion")]
        public bool DeleteQuestion(int id)
        {
            return _serviceFactory.Question.Delete(id);
        }


        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="model">Question model</param>
        /// <returns>True or False</returns>
        [HttpPut("UpdateQuestion")]
        public bool UpdateQuestion(QuestionModel model)
        {
            return _serviceFactory.Question.Update(model);
        }
    }
}