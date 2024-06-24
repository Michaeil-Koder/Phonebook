using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;

namespace Phonebook.Presentation.Controllers
{
    [Route("")]
    [ApiController]
    [ProducesResponseType(typeof(BaseCommandResponse), 200)]
    [ProducesResponseType(typeof(BaseCommandResponse), 400)]
    [ProducesResponseType(typeof(BaseCommandResponse), 401)]
    [ProducesResponseType(typeof(BaseCommandResponse), 403)]
    public class ApiControllerBase : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator=>_mediator??=HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
