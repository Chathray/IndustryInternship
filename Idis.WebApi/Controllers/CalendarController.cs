using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/Calendar")]
    public class CalendarController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IInternService _internService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;


        public CalendarController(IMapper mapper, IInternService internService, IEventService eventService, IEventTypeService eventTypeService)
        {
            _mapper = mapper;

            _internService = internService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
        }


        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="model">Event info model</param>
        /// <param name="creatorId">User Id, who create</param>
        /// <returns>Status code</returns>
        [HttpPost("CreateEvent")]
        public IActionResult CreateEvent(EventModel model, int creatorId)
        {
            var even = _mapper.Map<Application.EventModel>(model);
            even.CreatedBy = creatorId;

            var ok = _eventService.InsertEvent(even);

            if (!ok) Response.StatusCode = 500;

            return Ok();
        }


        /// <summary>
        /// Get whitelist
        /// </summary>
        /// <remarks>Get list of intern by format [src,value]</remarks>
        /// <returns>Dynamic object</returns>
        [HttpGet("GetWhitelist")]
        public dynamic GetWhitelist()
        {
            var guests = _internService.GetWhitelist();
            return guests;
        }


        /// <summary>
        /// Get event types
        /// </summary>
        /// <remarks>Get all event types list</remarks>
        /// <returns>List of event type</returns>
        [Produces("application/json")]
        [HttpGet("GetEventTypes")]
        public IList<EventTypeModel> GetEventTypes()
        {
            var eventypes = _eventTypeService.GetAll();
            return eventypes;
        }


        /// <summary>
        /// Get events
        /// </summary>
        /// <remarks>Get all event list</remarks>
        /// <returns>List of event</returns>
        /// <response code="201">Empty data</response>
        [ProducesResponseType(typeof(string), 201)]
        [HttpGet("GetEvents")]
        public string GetEvents()
        {
            return _eventService.GetJson();
        }


        /// <summary>
        /// Get joint events
        /// </summary>
        /// <remarks>Get joint events given intern Id</remarks>
        /// <param name="internId">Intern Id</param>
        /// <returns>List of joint event of a intern</returns>
        [HttpGet("GetJointEvents")]
        public string GetJointEvents(int internId)
        {
            var data = _eventService.GetEventsIntern();
            JArray array = new();

            foreach (DataRow i in data.Rows)
            {
                var json = i["Joined"].ToString().Split(',', '[', ']', ' ');

                foreach (var token in json)
                {
                    //_logger.LogInformation(token + ", " + iid);
                    if (token == internId.ToString())
                    {
                        array.Add(new JValue(i["Title"]));
                        break;
                    }
                }
            }
            return array.ToString();
        }
    }
}