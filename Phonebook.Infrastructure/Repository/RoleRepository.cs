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
    public class RoleRepository:GenericRepository<Roles>,IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<bool> ExistRole(string role)
        {
            return await _dbContext.Roles.AnyAsync(r => r.Role == role);
        }
    }
}
