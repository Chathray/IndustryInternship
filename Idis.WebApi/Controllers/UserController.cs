using AutoMapper;
using Idis.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Idis.WebApi
{
    [ApiController]
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IMapper mapper, IUserService userService,
            IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Validate user access
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Successful Authentication</response>
        /// <response code="500">Oops! Can't check your info right now</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [MapToApiVersion("1")]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Username or password is incorrect");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                user.UserId,
                Username = user.FirstName + " " + user.LastName,
                user.FirstName,
                user.LastName,
                tokenString
            });
        }


        /// <summary>
        /// New user account registration
        /// </summary>
        /// <remarks>Create a user account in Idis!</remarks>
        /// <param name="model" example="123">The user model</param>
        /// <response code="200">Successful registration</response>
        /// <response code="500">Oops! Can't register your info right now</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [MapToApiVersion("1")]
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                var userModel = _mapper.Map<UserModel>(model);
                // create user
                var done = _userService.Create(userModel);

                if (done)
                    return Ok();
                else
                    return StatusCode(500);
            }
            catch (ApiException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Retrieves a list of User
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Retrieved list of existing users in the system</response>
        [ProducesResponseType(typeof(IList<UserModel>), 200)]
        [Authorize]
        [MapToApiVersion("1")]
        [Produces("application/json")]
        [HttpGet("UserList")]
        public IList<UserModel> UserList()
        {
            var users = _userService.GetAll();
            return users;
        }


        /// <summary>
        /// Delete a user
        /// </summary>
        /// <remarks>Delete a user given UserId</remarks>
        /// <param name="id">User Id</param>
        /// <returns>True or False</returns>
        [MapToApiVersion("1")]
        [HttpDelete("DeleteUser")]
        public bool DeleteUser(int id)
        {
            return _userService.UserDelete(id);
        }


        /// <summary>
        /// Update password
        /// </summary>
        /// <remarks>Update password given User Id</remarks>
        /// <param name="model">Password update model</param>
        /// <returns>Status code: 200, 400 or 500</returns>
        [HttpPatch("UpdatePassword")]
        [MapToApiVersion("1")]
        public IActionResult UserUpdatePassword(PasswordUpdateModel model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
                return StatusCode(StatusCodes.Status400BadRequest);

            var email = User.Claims.ElementAt(0).Value;
            UserModel user = _userService.Authenticate(email, model.CurrentPassword);

            if (user is null) return StatusCode(StatusCodes.Status500InternalServerError);
            else
            {
                bool result = _userService.UpdatePassword(user.UserId, model.NewPassword);
                if (result) return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }
    }
}
