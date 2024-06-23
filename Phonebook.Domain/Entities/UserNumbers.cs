using Phonebook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Entities
{
    public class UserNumbers : BaseEntity
    {
        public string Title {get; set;}
        public string Phone { get; set;}

        public Guid UserId { get; set;}
        public User User { get; set;}
    }
}
