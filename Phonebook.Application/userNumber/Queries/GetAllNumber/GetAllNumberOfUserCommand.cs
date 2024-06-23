using MediatR;
using Phonebook.Application.DTOs;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Queries.GetAllNumber
{
    public class GetAllNumberOfUserCommand : IRequest<BaseCommandResponse>
    {
    }
}
