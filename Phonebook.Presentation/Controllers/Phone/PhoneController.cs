using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Responses;
using Phonebook.Application.userNumber.Commands.CreateUserNumbers;
using Phonebook.Application.userNumber.Commands.DeleteUserNumber;
using Phonebook.Application.userNumber.Commands.UpdateNumbers;
using Phonebook.Application.userNumber.Queries.GetAllNumber;
using Phonebook.Application.userNumber.Queries.GetOneNumber;

namespace Phonebook.Presentation.Controllers.Phone
{
    [Route("phone")]
    public class PhoneController : ApiControllerBase
    {

        [HttpPost("add")]
        
        public async Task<ActionResult<BaseCommandResponse>> AddPhone([FromBody] CreateUserNumbersCommand phone)
        {

            try
            {
                var result = await Mediator.Send(phone);
                if (result.Status == 401)
                {
                    var response = new BaseCommandResponse();
                    response.Success = false;
                    response.Message = "برای دسترسی به این بخش ورود/ثبت نام انجام دهید";

                    return Unauthorized(response);
                }
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }catch (Exception ex)
            {
                var response = new BaseCommandResponse();
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpGet]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            var result = await Mediator.Send(new GetAllNumberOfUserCommand());
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            else if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{Title}")]
        
        public async Task<ActionResult<BaseCommandResponse>> GetOneNumber([FromRoute] string Title)
        {
            var result = await Mediator.Send(new GetOneNumberCommand() { Title=Title});
            if (result.Status == 404)
            {
                return NotFound(result);
            }
            else if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateNumberHandler([FromBody] UpdateUserNumbersCommand numbersCommand)
        {
            try
            {
                var result = await Mediator.Send(numbersCommand);
                if (result.Status == 404)
                {
                    return NotFound(result);
                }
                else if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new BaseCommandResponse();
                response.Status = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }



        [HttpDelete("remove/{Title}")]
        
        public async Task<ActionResult<BaseCommandResponse>> DeleteNumberHandler([FromRoute] string Title)
        {
            try
            {
                var result = await Mediator.Send(new DeleteUserNumberCommand() { Title=Title });
                if (result.Status == 404)
                {
                    return NotFound(result);
                }
                else if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new BaseCommandResponse();
                response.Status = 400;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
