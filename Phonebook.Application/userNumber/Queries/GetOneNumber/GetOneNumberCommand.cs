using MediatR;
using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.userNumber.Queries.GetOneNumber
{
    public class GetOneNumberCommand :IRequest<BaseCommandResponse>
    {
        public string Title { get; set; }
    }
}
