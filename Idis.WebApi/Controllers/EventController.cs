using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IInternService _internService;
        private readonly IEventService _eventService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IOriginService _origin;


        public EventController(IMapper mapper, IInternService internService, IEventService eventService, IEventTypeService eventTypeService, IOriginService origin)
        {
            _mapper = mapper;
            _origin = origin;
            _internService = internService;
            _eventService = eventService;
            _eventTypeService = eventTypeService;
        }


        /// <summary>
        /// Create or update a event
        /// </summary>
        /// <param name="model">Event info model</param>
        /// <returns>Status code</returns>
        [HttpPut]
        public IActionResult CreateEvent(CreateEventRequest model)
        {
            var even = _mapper.Map<EventModel>(model);
            even.CreatedBy = _origin.UserId;

            var ok = _eventService.InsertEvent(even);

            if (!ok) Response.StatusCode = 500;

            return Ok();
        }


        /// <summary>
        /// Delete a event
        /// </summary>
        /// <param name="evenId" example="1">Id of an event</param>
        /// <returns>Status code: 200, 401</returns>
        [HttpDelete]
        public IActionResult DeleteEvent(int evenId)
        {
            bool result = _eventService.Delete(evenId);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else return NotFound();
        }


        /// <summary>
        /// Get events
        /// </summary>
        /// <remarks>Get all event list</remarks>
        /// <returns>List of event</returns>
        [HttpGet("Events")]
        public IActionResult GetEvents()
        {
            var events = _eventService.GetJson();
            if (events is null)
                return NoContent();
            else
                return Content(events.JsonPrettify());
        }


        /// <summary>
        /// Get whitelist
        /// </summary>
        /// <remarks>Get list of intern by format [src,value]</remarks>
        /// <returns>Dynamic object</returns>
        [HttpGet("Whitelist")]
        public IActionResult GetWhitelist()
        {
            var guests = _internService.GetWhitelist();
            return Ok(guests);
        }


        /// <summary>
        /// Get event types
        /// </summary>
        /// <remarks>Get all event types list</remarks>
        /// <returns>List of event type</returns>
        [Produces("application/json")]
        [HttpGet("EventTypes")]
        public IList<EventTypeModel> GetEventTypes()
        {
            var eventypes = _eventTypeService.GetAll();
            return eventypes;
        }


        /// <summary>
        /// Get joint events of a intern
        /// </summary>
        /// <remarks>Get joint events given intern Id</remarks>
        /// <param name="internId">Intern Id</param>
        /// <returns>List of joint event of a intern</returns>
        [HttpGet("JointEvents")]
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