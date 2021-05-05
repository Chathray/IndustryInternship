using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Idis.Website
{
    [Route("/[action]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceFactory _serviceFactory;
        private readonly IHubContext<EchoHub> _hubContext;

        public UserController(IMapper mapper, IServiceFactory serviceFactory, IHubContext<EchoHub> hubContext)
        {
            _serviceFactory = serviceFactory;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Authentication()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            SetSessionInfo();
            int userId = int.Parse(ViewBag.id);

            DataTable user = _serviceFactory.User.GetProfile(userId);
            var model = DataExtensions.GetItem<ProfileViewModel>(user.Rows[0]);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Settings()
        {
            SetSessionInfo();
            int userId = int.Parse(ViewBag.id);

            UserModel user = _serviceFactory.User.GetOne(userId);
            SettingsViewModel model = _mapper.Map<SettingsViewModel>(user);
            model.Departments = _serviceFactory.Department.GetAll();
            return View(model);
        }

        private void SetSessionInfo()
        {
            ViewBag.id = User.Claims.ElementAt(0).Value;
            ViewBag.email = User.Claims.ElementAt(1).Value;
            ViewBag.fullname = User.Claims.ElementAt(2).Value;
            ViewBag.role = User.Claims.ElementAt(3).Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (!ModelState.IsValid) goto Fail;

            UserModel user = _serviceFactory.User.Authenticate(model.LoginEmail, model.LoginPassword);

            if (user == null) goto Fail;

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.FirstName +" " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // Pass to index ViewResult
            TempData["avatar"] = user.Avatar;
            TempData["status"] = user.Status;

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = model.Remember,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                });
            goto Done;

        Done:
            return Redirect("/");
        Fail:
            ModelState.AddModelError("fail", "The username or password is incorrect!");
            return View("Authentication", model);
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            try
            {
                bool result = _serviceFactory.User.InsertUser(user);
                if (result) goto Done;
            }
            catch (WebException ex)
            {
                Log.Error(ex.Message);
            }
            goto Fail;

        Done:
            ModelState.AddModelError("done", "Registration complete, login now!");
            return View("Authentication");
        Fail:
            ModelState.AddModelError("fail", "Registration failed, please try again!");
            return View("Authentication", model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Authentication");
        }

        [HttpPost("/User/UpdateBasic")]
        public IActionResult UserUpdateBasic(SettingsViewModel model)
        {
            var user = _mapper.Map<UserModel>(model);
            var userId = User.Claims.ElementAt(0).Value;
            user.UserId = int.Parse(userId);

            bool result = _serviceFactory.User.UpdateBasic(user);

            TempData["notification"] = "Update basic information: " + result;
            return Redirect("/Settings");
        }

        [HttpPost("/User/UpdatePassword")]
        public IActionResult UserUpdatePassword(PasswordUpdateModel model)
        {
            if (model.NewPassword != model.ConfirmNewPassword) goto Failed;

            var email = User.Claims.ElementAt(1).Value;
            UserModel user = _serviceFactory.User.Authenticate(email, model.CurrentPassword);

            if (user is null) goto Failed;
            else
            {
                bool result = _serviceFactory.User.UpdatePassword(user.UserId, model.NewPassword);
                if (result) goto Success;
            }

        Failed:
            TempData["notification"] = "Password change failed!";
            return Redirect("/Settings");
        Success:
            TempData["notification"] = "Password change success!";
            return Redirect("/Settings");
        }

        [HttpPost]
        public async Task<bool> UserDelete(int userId)
        {
            var result = _serviceFactory.User.UserDelete(userId);
            if (result)
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return result;
        }

        [HttpGet("/User/GetStatus")]
        [HttpPost("/User/SetStatus")]
        public async Task<dynamic> StatusAsync(string status)
        {
            var userId = int.Parse(User.Claims.ElementAt(0).Value);

            if (Request.Method == "POST")
            {
                var result = _serviceFactory.User.SetStatus(userId, status);
                if (result)
                    await _hubContext.Clients.User(userId + "")
                        .SendAsync("ClientStatus", status);

                return result;
            }
            else
                return _serviceFactory.User.GetOne(userId).Status;
        }
    }
}