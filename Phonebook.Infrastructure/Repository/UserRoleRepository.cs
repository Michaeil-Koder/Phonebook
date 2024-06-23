using Microsoft.EntityFrameworkCore;
using Phonebook.Application.IRepository;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Infrastructure.Repository
{
    public class UserRoleRepository : GenericRepository<UserRole>,IUserRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRoleRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsAdmin(Guid userId)
        {
            return await _dbContext.UserRoles.Where(ur=>ur.UserId == userId && ur.Roles.Role=="ادمین").AnyAsync();
                
                
        }
    }
}
