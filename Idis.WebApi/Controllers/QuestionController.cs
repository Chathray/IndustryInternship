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
    [Route("api/v{version:apiVersion}/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;

        public QuestionController(IMapper mapper, IServiceFactory factory)
        {
            _mapper = mapper;
            _serviceFactory = factory;
        }


        /// <summary>
        /// Get a question
        /// </summary>
        /// <param name="id">Question Id</param>
        [HttpGet]
        public IActionResult GetQuestion(int id)
        {
            var ques = _serviceFactory.Question.GetOne(id);
            if (ques != null)
                return Ok(ques);
            else
                return NotFound();
        }



        /// <summary>
        /// Create or update a question
        /// </summary>
        /// <param name="model">Question model</param>
        /// <returns>True or False</returns>
        [HttpPut]
        public IActionResult UpdateQuestion(CreateQuestionRequest model)
        {
            var ques = _mapper.Map<QuestionModel>(model);

            if (model.QuestionId is null)
            {
                ques.QuestionId = null;
                var created = _serviceFactory.Question.Create(ques);
                return new JsonResult(new { created });
            }
            else
            {
                var updated = _serviceFactory.Question.Update(ques);
                return new JsonResult(new { updated });
            }
        }


        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">Question Id</param>
        /// <returns>True or False</returns>
        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            var deleted = _serviceFactory.Question.Delete(id);
            return new JsonResult(new { deleted });
        }
    }
}