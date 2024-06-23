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
    public class UserNumbersRepository:GenericRepository<UserNumbers>,IUserNumbersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserNumbersRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<bool> ExistTitlePhone(Guid UserId, string Title, string Phone)
        {
            return await _dbContext.UserNumbers.AnyAsync(cu => cu.UserId == UserId && cu.Title == Title && cu.Phone == Phone);
        }

        public async Task<UserNumbers> GetOneNumberByUserIdAndTitle(Guid UserId, string Title)
        {
            return await _dbContext.UserNumbers.Where(un => un.UserId == UserId && un.Title == Title).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<UserNumbers>> GetUserNumbersByUserId(Guid UserId)
        {
            return await _dbContext.UserNumbers.Where(un=>un.UserId == UserId).ToListAsync();
        }
    }
}
