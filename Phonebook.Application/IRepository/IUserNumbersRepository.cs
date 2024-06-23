using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.IRepository
{
    public interface IUserNumbersRepository : IGenericRepository<UserNumbers>
    {
        Task<bool> ExistTitlePhone(Guid UserId, string Title, string Phone);
        Task<IReadOnlyList<UserNumbers>> GetUserNumbersByUserId(Guid UserId);
        Task<UserNumbers> GetOneNumberByUserIdAndTitle(Guid UserId, string Title);
    }
}
