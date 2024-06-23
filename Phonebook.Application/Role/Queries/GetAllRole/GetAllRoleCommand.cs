using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Role.Queries.GetAllRole
{
    public class GetAllRoleCommand:IRequest<BaseCommandResponse>
    {
    }
}
