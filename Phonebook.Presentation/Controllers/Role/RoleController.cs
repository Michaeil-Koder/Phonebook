using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;
using Phonebook.Application.Role.Commands.AddRole;
using Phonebook.Application.Role.Commands.EditRole;
using Phonebook.Application.Role.Queries.GetAllRole;

namespace Phonebook.Presentation.Controllers.Role
{
    [Route("role")]
    public class RoleController : ApiControllerBase
    {

        [HttpPost("add")]
     
        public async Task<ActionResult<BaseCommandResponse>> AddRoleHandler([FromBody] CreateRoleCommand role)
        {
            var result = await Mediator.Send(role);
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


        [HttpPut("update/{id:guid}")]
        
        public async Task<ActionResult<BaseCommandResponse>> UpdateRoleHandler([FromBody] string role, [FromRoute] Guid id)
        {

            var result = await Mediator.Send(new EditRoleCommand() { RoleId=id,RoleName=role});
            if (result.Status == 403)
            {
                return StatusCode(403, result);
            }
            else if (result.Status == 401)
            {
                return Unauthorized(result);
            }
            else if (!result.Success)
            {
                return BadRequest(result);
            }
            return StatusCode(201, result);
        }


        [HttpGet]
        
        public async Task<ActionResult<BaseCommandResponse>> GetAllRole()
        {

            var result = await Mediator.Send(new GetAllRoleCommand());
            if (result.Status == 403)
            {
                return StatusCode(403, result);
            }
            else if (result.Status == 401)
            {
                return Unauthorized(result);
            }
            else if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
