using Phonebook.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Identity.CheckToken
{
    public interface IChekSign_ExpToken
    {
        public Task<BaseCommandResponse> Check();
    }
}
