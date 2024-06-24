using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;
using Phonebook.Application.Users.Queries.GetAllUser;

namespace Phonebook.Presentation.Controllers.Users
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {

        [HttpGet]
        
        public async Task<ActionResult<BaseCommandResponse>> GetAllUser()
        {
            var Result = await Mediator.Send(new GetAllUserCommand());
            if (Result.Status == 401)
            {
                return StatusCode(401, Result);
            }else if (Result.Status == 403)
            {
                return StatusCode(403,Result);
            }else if (!Result.Success)
            {
                return BadRequest(Result);
            }
            return Ok(Result);
        }
    }
}
