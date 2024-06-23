﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;
using Phonebook.Application.Role.Commands.AddRole;

namespace Phonebook.Presentation.Controllers.Role
{
    [Route("role")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(BaseCommandResponse),201)]
        [ProducesResponseType(typeof(BaseCommandResponse),400)]
        [ProducesResponseType(typeof(BaseCommandResponse),401)]
        [ProducesResponseType(typeof(BaseCommandResponse),403)]
        public async Task<ActionResult<BaseCommandResponse>> AddRoleHandler([FromBody] CreateRoleCommand role)
        {
            var result = await _mediator.Send(role);
            if (result.Status == 403)
            {
                return StatusCode(403,result);
            }else if(result.Status == 401)
            {
                return Unauthorized(result);
            }
            else if(!result.Success)
            {
                return BadRequest(result);
            }
            return StatusCode(201,result);
        }
    }
}