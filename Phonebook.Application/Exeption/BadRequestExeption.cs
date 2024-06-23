using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Exeption
{
    public class BadRequestExeption : ApplicationException
    {
        public BadRequestExeption(string message) : base(message)
        {

        }
    }
}
