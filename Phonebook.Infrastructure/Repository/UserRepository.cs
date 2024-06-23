using Microsoft.EntityFrameworkCore;
using Phonebook.Application.IRepository;
using Phonebook.Domain.Entities;
using Phonebook.Infrastructure.AppDbContext;

namespace Phonebook.Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext applicationDbContext) :base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<bool> ExistsNationalCode(string code)
        {
           return await _dbContext.Users.AnyAsync(u=>u.NationalCode == code);
        }

        public async Task<int> GetCount()
        {
            return await _dbContext.Users.CountAsync();
        }

        public async Task<User> GetUserByNationalCode(string NationalCode)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.NationalCode == NationalCode);
        }

        public async Task<bool> IsAdmin(Guid userId)
        {
            return  true;
        }
    }
}
