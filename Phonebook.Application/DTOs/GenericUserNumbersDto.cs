using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.DTOs
{
    public class GenericUserNumbersDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
    }
}
