using MediatR;
using Phonebook.Application.Responses;
using Phonebook.Domain.Enums;


namespace Phonebook.Application.Authentication.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseCommandResponse>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string NationalCode { get; set; } 
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public Dictionary<string, string> TitlePhone { get; set; }
        //public Array Title { get; set; }
        //public Array Phone { get; set; }
    }
}
