using Phonebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.IRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<bool> ExistsNationalCode(string code);
        Task<User> GetUserByNationalCode(string NationalCode);
        Task<int> GetCount();
        Task<bool> IsAdmin(Guid userId);
    }
}
