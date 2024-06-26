﻿using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Authentication.Commands.CreateUser;
using MediatR;
using Phonebook.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Phonebook.Application.Authentication.Queries.Login;

namespace Phonebook.Presentation.Controllers.Authentication
{
    [Route("auth")]
    //[Authorize]
    public class AuthController : ApiControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<BaseCommandResponse>> UserRegister([FromBody] CreateUserCommand userCommand)
        {
            if (userCommand == null)
            {
                var response = new BaseCommandResponse();
                response.Message = "لطفا تمام مقادیر مورد نیاز را وارد کنید";
                response.Success = false;
                return BadRequest(response);
            }
            var result = await Mediator.Send(userCommand);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            Response.Cookies.Append("Token", result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(int.Parse(_configuration["Jwt:Day"]))
            });

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<ActionResult<BaseCommandResponse>> Login([FromBody] LoginUserCommand user)
        {
            try
            {
                var result = await Mediator.Send(user);
                if (result.Status == 403)
                {
                    return Forbid(result.Message);
                }
                if (!result.Success)
                {
                    return NotFound(result);
                }

                Response.Cookies.Append("Token", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(int.Parse(_configuration["Jwt:Day"]))
                });

                return Ok(result);
            }
            catch (Exception ex) 
            {
                var response = new BaseCommandResponse();
                response.Message = ex.Message;
                response.Success = false;
                return BadRequest(response);
            }
        }
    }
}
