using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Commands.CreateUserNumbers
{
    public class CreateUserNumbersCommand : IRequest<BaseCommandResponse>
    {
        public Dictionary<string, string> TitlePhone { get; set; }
    }
}
