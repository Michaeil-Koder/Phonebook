using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.IRepository
{
    public interface IUserRoleRepository : IGenericRepository<UserRole>
    {
        Task<bool> IsAdmin(Guid userId);
        Task<bool> ExistUserRoleByUserId_RoleId(Guid userId,Guid roleId);
    }
}
