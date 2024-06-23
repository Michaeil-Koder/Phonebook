using Phonebook.Domain.Common;
using Phonebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Domain.Entities
{
    public class Roles:BaseEntity
    {
        public string Role { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
