using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;

namespace Idis.Website
{
    [Authorize]
    public class CalendarController : Controller
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            ViewBag.id = User.Claims.ElementAt(0).Value;
            ViewBag.email = User.Claims.ElementAt(1).Value;
            ViewBag.fullname = User.Claims.ElementAt(2).Value;
            ViewBag.status = User.Claims.ElementAt(3).Value;
        }

        [HttpGet("/Calendar")]
        public IActionResult Calendar()
        {
            ViewData["page-2"] = "active";

            var guests = _internService.GetWhitelist();
            var eventype = _eventTypeService.GetAll();

            var model = new CalendarViewModel(eventype, guests)
            {
                Creator = ViewBag.fullname
            };

            return View(model);
        }

        [AcceptVerbs("POST")]
        public IActionResult CreateEvent(CalendarViewModel model)
        {
            var even = _mapper.Map<EventModel>(model);
            even.CreatedBy = int.Parse(ViewBag.id);

            var ok = _eventService.InsertEvent(even);

            if (!ok) Response.StatusCode = -1;

            return RedirectToAction("Calendar");
        }

        [HttpPost]
        public IActionResult DeleteEvent(string evenTitle)
        {
            bool result = _eventService.DeleteByTitle(evenTitle);
            if (result)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else return StatusCode(StatusCodes.Status501NotImplemented);
        }

        [HttpGet]
        public string GetEvents()
        {
            return _eventService.GetJson();
        }

        [HttpGet]
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