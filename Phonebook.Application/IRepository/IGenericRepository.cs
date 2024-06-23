using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid Id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Create (T entity);
        Task<bool> Update (T entity);
        Task<bool> Delete (Guid Id);
        Task<bool> Exist (Guid Id);
    }
}
