using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Application.Responses;

namespace Phonebook.Application.userNumber.Commands.UpdateNumbers
{
    public class UpdateUserNumbersCommand:IRequest<BaseCommandResponse>
    {
        public GetUserNumberDto GetUserNumberDto { get; set; }
    }
}
