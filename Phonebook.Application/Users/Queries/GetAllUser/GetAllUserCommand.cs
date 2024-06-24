using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Users.Queries.GetAllUser
{
    public class GetAllUserCommand:IRequest<BaseCommandResponse>
    {
    }
}
