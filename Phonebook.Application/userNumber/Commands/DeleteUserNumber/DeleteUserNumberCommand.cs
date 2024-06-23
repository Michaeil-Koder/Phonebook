using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Commands.DeleteUserNumber
{
    public class DeleteUserNumberCommand:IRequest<BaseCommandResponse>
    {
        public string Title { get; set; }
    }
}
