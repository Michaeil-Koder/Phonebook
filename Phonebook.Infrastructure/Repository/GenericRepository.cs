using Microsoft.EntityFrameworkCore;
using Phonebook.Application.IRepository;
using Phonebook.Infrastructure.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<T> Create(T entity)
        {
            await _applicationDbContext.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var entity=await Get(id);

                if (entity==null) return false;

                _applicationDbContext.Set<T>().Remove(entity);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Exist(Guid Id)
        {
            var entity=await Get(Id);
            return entity !=null;
        }

        public async Task<T> Get(Guid Id)
        {
           return await _applicationDbContext.Set<T>().FindAsync(Id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
           return await _applicationDbContext.Set<T>().ToListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            try
            {
                _applicationDbContext.Entry(entity).State = EntityState.Modified;
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
