using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Commands.AddRole
{
    public class CreateRoleCommand : IRequest<BaseCommandResponse>
    {
        public string Role {  get; set; }
    }
}
