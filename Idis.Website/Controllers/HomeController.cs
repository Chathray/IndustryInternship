using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace Idis.Website
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;

        public HomeController(IMapper mapper, IServiceFactory serviceFactory)
        {
            _mapper = mapper;
            _serviceFactory = serviceFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string[] allow = { "GET", "POST" };
            if (allow.Contains(context.HttpContext.Request.Method))
            {
                // By Calling in View
                // @ViewBag.email or @ViewData["email"], they are same

                ViewBag.id = User.Claims.ElementAt(0).Value;
                ViewBag.email = User.Claims.ElementAt(1).Value;
                ViewBag.fullname = User.Claims.ElementAt(2).Value;
                ViewBag.role = User.Claims.ElementAt(3).Value;
            }
        }

        [HttpGet]
        public IActionResult Index(
            int page = 1, int size = 7, int sort = 1,
            int search_on = 0, string search_string = "",
            int on_passed = 2,
            int date_filter = 0, string start_date = "1970-01-01", string end_date = "2070-01-01")
        {
            ViewData["page-1"] = "active";

            DataSet internList;

            bool haveFilter = on_passed != 2 || date_filter != 0;

            if (haveFilter)
            {
                internList = _serviceFactory.Intern.GetInternByPage(
                     page, size, sort,
                     search_on, search_string,
                     on_passed,
                     date_filter, start_date, end_date);
            }
            else
                internList = _serviceFactory.Intern.GetInternByPage(
                     page, size, sort,
                     search_on, search_string);

            var total = Convert.ToInt32(internList.Tables[1].Rows[0]["FOUND_ROWS"]);
            var pagination = new PaginationLogic(total, page, size);

            var model = new IndexViewModel(pagination, internList);

            ViewData["trainings.count"] = CountByIndex(8) - 1; // Prevent default training: 'None'
            ViewData["organizations.count"] = CountByIndex(5);
            ViewData["departments.count"] = CountByIndex(1);
            ViewData["points.count"] = CountByIndex(6);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            var intern = _mapper.Map<InternModel>(model);
            intern.MentorId = int.Parse(ViewBag.id);

            if (intern.Avatar is null)
            {
                intern.Avatar = "_intern.jpg";
            }

            try
            {
                _serviceFactory.Intern.Create(intern);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Redirect("/");
        }

        [HttpPost("InternUpdate/{id}")]
        public IActionResult Index(IndexViewModel model, int id)
        {
            var intern = _mapper.Map<InternModel>(model);
            intern.MentorId = int.Parse(ViewBag.id);
            intern.InternId = id;

            if (intern.Avatar == "")
            {
                intern.Avatar = "_intern.jpg";
            }

            try
            {
                _serviceFactory.Intern.Update(intern);
            }
            catch (Exception)
            {
                Response.StatusCode = -1;
            }

            return Redirect("/");
        }

        [HttpGet("/Question")]
        public IActionResult Question()
        {
            ViewData["page-3"] = "active";

            var model = _serviceFactory.Question.GetAll()
                .OrderBy(o => o.Group)
                .ToList();

            return View(model);
        }


        #region INSERT
        [HttpPost]
        public bool InsertQuestion(QuestionViewModel model)
        {
            var question = _mapper.Map<QuestionModel>(model);
            return _serviceFactory.Question.Create(question);
        }

        [HttpPost]
        public bool EvaluateIntern(PointViewModel model)
        {
            var point = _mapper.Map<PointModel>(model);
            point.MarkerId = int.Parse(ViewBag.id);
            return _serviceFactory.Point.EvaluateIntern(point);
        }

        [HttpPost]
        public bool InsertTraining(TrainingModel model)
        {
            model.CreatedBy = int.Parse(ViewBag.id);
            return _serviceFactory.Training.Create(model);
        }
        #endregion



        #region DELETE
        [HttpPost]
        public bool DeleteQuestion(int id)
        {
            return _serviceFactory.Question.Delete(id);
        }
        [HttpPost]
        public bool DeleteIntern(int id)
        {
            return _serviceFactory.Intern.Delete(id);
        }
        [HttpPost]
        public bool DeletePoint(int id)
        {
            return _serviceFactory.Point.Delete(id);
        }
        [HttpPost]
        public bool DeleteOrganization(int id)
        {
            return _serviceFactory.Organization.Delete(id);
        }
        [HttpPost]
        public bool DeleteDepartment(int id)
        {
            return _serviceFactory.Department.Delete(id);
        }
        [HttpPost]
        public bool DeleteTraining(int id)
        {
            var result = _serviceFactory.Training.Delete(id);
            if (result)
            {
                _serviceFactory.Department.RefreshSharedTrainings(id);
            }
            return result;
        }
        #endregion



        #region UPDATE
        [HttpPost]
        public bool SetSharedTraining(int sharedId, int[] depArray)
        {
            bool result = true;
            foreach (var depId in depArray)
            {
                result = result && _serviceFactory.Department.InsertSharedTraining(sharedId, depId);
            }
            return result;
        }
        [HttpPost]
        public bool UpdatePoint(PointModel model)
        {
            model.MarkerId = int.Parse(ViewBag.id);
            return _serviceFactory.Point.Update(model);
        }
        [HttpPost]
        public bool UpdateQuestion(QuestionModel model)
        {
            return _serviceFactory.Question.Update(model);
        }
        [HttpPost]
        public bool UpdateDepartment(DepartmentModel model)
        {
            if (model.DepartmentId == -1)
            {
                model.DepartmentId = null;
                return _serviceFactory.Department.Create(model);
            }
            else
            {
                string[] ignores = WebConstants.SharedTrainings;
                return _serviceFactory.Department.Update(model, ignores);
            }
        }
        [HttpPost]
        public bool UpdateOrganization(OrganizationModel model)
        {
            if (model.OrganizationId == -1)
            {
                model.OrganizationId = null;
                return _serviceFactory.Organization.Create(model);
            }
            else
                return _serviceFactory.Organization.Update(model);
        }
        [HttpPost]
        public bool UpdateTraining(TrainingModel model)
        {
            model.CreatedBy = int.Parse(ViewBag.id);
            return _serviceFactory.Training.Update(model);
        }
        [HttpPost]
        public bool UploadAvatar(string ImgStr, string ImgName)
        {
            return SaveImage(ImgStr, ImgName);
        }
        #endregion



        #region GET
        [HttpGet("CountByIndex/{index}")]
        public int CountByIndex(int index)
        {
            return _serviceFactory.User.CountByIndex(index);
        }

        [HttpGet("Home/GetInternInfo")]
        [HttpGet("GetInternInfo/{id}")]
        public string GetInternInfo(int id)
        {
            var json = _serviceFactory.Intern.GetInternInfo(id);
            return json;
        }

        [HttpGet]
        public string GetInternDetail(int id)
        {
            var json = _serviceFactory.Intern.GetInternDetail(id);
            return json;
        }

        [HttpGet]
        public string GetTrainingContent(int id)
        {
            return _serviceFactory.Training.GetOne(id)
                .TraData;
        }

        [HttpGet]
        public string GetPassedCount()
        {
            var passed = _serviceFactory.Point.GetPassedCount();
            var total = CountByIndex(6);
            return (passed / (float)total).ToString("0%");
        }
        [HttpGet]
        [Produces("application/json")]
        public IList<TrainingModel> GetJointTrainings(int internId)
        {
            var obj = _serviceFactory.Intern.GetJointTrainings(internId);
            return obj;
        }
        //_____________________________________________________

        [HttpGet]
        public IActionResult GetPoint(int id, bool withName)
        {
            dynamic obj;
            if (withName)
                obj = _serviceFactory.Point.GetPointDetail(id);
            else
                obj = _serviceFactory.Point.GetOne(id);

            return Ok(obj);
        }
        [HttpGet]
        public IActionResult GetQuestion(int id)
        {
            return Ok(_serviceFactory.Question.GetOne(id));
        }
        //_____________________________________________________
        [HttpGet]
        public IActionResult GetTrainingManagerData()
        {
            var TrainingList = _serviceFactory.Training.GetAll();
            TrainingList.RemoveAt(0);

            ExpandoObject list = new();
            list.TryAdd("Training", TrainingList);
            list.TryAdd("Department", _serviceFactory.Department.GetAll());

            return Json(list);
        }
        [HttpGet]
        public IActionResult GetAllDynamic(string[] fields)
        {
            ExpandoObject list = new();
            foreach (var field in fields)
            {
                list.TryAdd(field, _serviceFactory.GetAll(field));
            }
            return Ok(list);
        }
        #endregion


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // For all wrong routes
        [Route("/Welcome")]
        public IActionResult Welcome()
        {
            return View();
        }

        public bool SaveImage(string ImgStr, string ImgName)
        {
            string path = Environment.CurrentDirectory + "\\wwwroot\\img\\avatar"; //Path

            //Check if directory exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            //set the image path
            string imgPath = Path.Combine(path, ImgName);

            try
            {
                byte[] imageBytes = Convert.FromBase64String(ImgStr);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
