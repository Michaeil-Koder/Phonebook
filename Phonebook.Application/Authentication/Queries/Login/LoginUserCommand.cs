using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Authentication.Queries.Login
{
    public class LoginUserCommand : IRequest<BaseCommandResponse>
    {
        public string NationalCode { get; set; }
        public string Password { get; set; }
    }
}
