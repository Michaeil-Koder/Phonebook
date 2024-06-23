using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.UserOfRole.Commands.GivingRole
{
    public class GivingRoleCommand:IRequest<BaseCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
