using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Identity.JsonWebToken
{
    public interface IJsonWebToken
    {
        public string GenerateToken(Guid Id);
    }
}
