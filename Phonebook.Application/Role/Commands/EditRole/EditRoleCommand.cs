using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Commands.EditRole
{
    public class EditRoleCommand:IRequest<BaseCommandResponse>
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
