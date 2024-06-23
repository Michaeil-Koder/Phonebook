using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;
using Phonebook.Application.UserOfRole.Commands.GivingRole;

namespace Phonebook.Presentation.Controllers.UserRole
{
    [Route("[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("giv_role")]
        [ProducesResponseType(typeof(BaseCommandResponse),201)]
        [ProducesResponseType(typeof(BaseCommandResponse),400)]
        [ProducesResponseType(typeof(BaseCommandResponse),401)]
        [ProducesResponseType(typeof(BaseCommandResponse),403)]
        public async Task<ActionResult<BaseCommandResponse>> givingRole([FromBody] GivingRoleCommand giving)
        {
            var result=await _mediator.Send(giving);
            if (result.Status == 401)
            {
                return StatusCode(401, result);
            }else if(result.Status == 403)
            {
                return StatusCode(403,result);
            }else if (!result.Success)
            {
                return BadRequest(result);
            }
            return StatusCode(201,result);
        }
    }
}
