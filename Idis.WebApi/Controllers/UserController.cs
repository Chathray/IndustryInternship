using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Idis.WebApi
{
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        private readonly IOriginService _origin;

        public UserController(IMapper mapper, IUserService userService,
            IOptions<AppSettings> appSettings, IOriginService origin)
        {
            _origin = origin;
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Get curent user info
        /// </summary>
        /// <remarks>Get current user info given userId</remarks>
        /// <returns>A user with specific info</returns>
        [Produces("application/json")]
        [HttpGet]
        public UserModel GetUser()
        {
            var user = _userService.GetOne(_origin.UserId);
            return user;
        }


        /// <summary>
        /// Delete a user
        /// </summary>
        /// <remarks>Delete a user given UserId</remarks>
        /// <param name="userId" example="2">User Id</param>
        /// <returns>True or False</returns>
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            var deleted = _userService.DeleteUser(userId);
            return new JsonResult(new { userId, deleted });
        }


        /// <summary>
        /// Retrieves a list of User
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Retrieved list of existing users in the system</response>
        [ProducesResponseType(typeof(IList<UserModel>), 200)]
        [Produces("application/json")]
        [HttpGet("Users")]
        public IList<UserModel> UserList()
        {
            var users = _userService.GetAll();
            return users;
        }


        /// <summary>
        /// Validate user access
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Successful Authentication</response>
        /// <response code="501">Username or password is incorrect</response>
        /// <response code="400">Username or password is is invalid</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(501)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return StatusCode(StatusCodes.Status501NotImplemented, "Username or password is incorrect.");

            var apiToken = "Bearer " + GenerateJwtToken(user);

            // Save user origin data
            _origin.User = user;

            return Content(apiToken);
        }


        /// <summary>
        /// New user account registration
        /// </summary>
        /// <remarks>
        /// Create a user account in Idis
        /// </remarks>
        /// <param name="model">The user model</param>
        /// <response code="200">Successful registration</response>
        /// <response code="500">Oops! Can't register your info right now</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            try
            {
                var userModel = _mapper.Map<UserModel>(model);
                // create user
                var done = _userService.InsertUser(userModel);

                if (done)
                    return Ok();
                else
                    return StatusCode(StatusCodes.Status501NotImplemented);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Update password
        /// </summary>
        /// <remarks>Update password given User Id</remarks>
        /// <param name="model">Password update model</param>
        /// <returns>Status code: 200, 400 or 500</returns>
        [HttpPatch("Password")]
        public IActionResult UserUpdatePassword(PasswordUpdateRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            UserModel user = _userService.Authenticate(_origin.Email, model.CurrentPassword);

            if (user is null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            else
            {
                bool result = _userService.UpdatePassword(user.UserId, model.NewPassword);
                if (result)
                    return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }


        /// <summary>
        /// Get profile complete value
        /// </summary>
        /// <param name="userId" example="3"></param>
        [HttpGet("ProfileValue")]
        public int GetProfileValue(int userId)
        {
            try
            {
                var result = _userService.GetProfileValue(userId);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetProfileValue));
                return -1;
            }
        }


        /// <summary>
        /// Get user status
        /// </summary>
        [HttpGet("Status")]
        public IActionResult GetUserStatus()
        {
            var user = _userService.GetOne(_origin.UserId);
            if (user != null)
                return Ok(new { user.Status });
            else
                return NotFound();
        }


        /// <summary>
        /// Update user status
        /// </summary>
        [HttpPatch("Status")]
        public IActionResult SetUserStatus(StatusRequest status)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model object");

            var success = _userService.SetStatus(_origin.UserId, status.Status);
            if (success)
                return Ok("Updated value: " + status.Status);
            else
                return StatusCode(StatusCodes.Status406NotAcceptable);
        }


        /// <summary>
        /// Count rows in a table/schema
        /// </summary>
        /// <remarks>
        /// Possible values are:
        /// 
        ///      1 : Departments,
        ///      2 : Events,
        ///      3 : EventTypes,
        ///      4 : Interns,
        ///      5 : Organizations,
        ///      6 : Points,
        ///      7 : Questions,
        ///      8 : Trainings,
        ///      9 : Users
        /// </remarks>
        /// <param name="index" example="4">Id of schema to count</param>
        /// <returns>Number of rows given table index</returns>
        [MapToApiVersion("2")]
        [HttpHead("CountByIndex/{index}")]
        public IActionResult CountByIndex(int index)
        {
            var count = _userService.CountByIndex(index);
            HttpContext.Response.Headers.Add("count-by-index", $"{count}");

            return NoContent();
        }
        [MapToApiVersion("2")]



        // Helper methods
        private string GenerateJwtToken(UserModel user)
        {
            // Generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("ID", user.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
