using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.IRepository
{
    public interface IRoleRepository : IGenericRepository<Roles>
    {
        Task<bool> ExistRole(string role);
    }
}
