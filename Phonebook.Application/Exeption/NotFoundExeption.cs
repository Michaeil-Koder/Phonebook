using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.Exeption
{
    public class NotFoundExeption : ApplicationException
    {
        public NotFoundExeption(string name,object key) :base($"{name} ({key}) یافت نشد.")
        {

        }
    }
}
