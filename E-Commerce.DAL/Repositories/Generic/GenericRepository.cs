using E_Commerce.DAL.Data.Context;
using E_Commerce.DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyAppContext _dbContext;

        public GenericRepository(MyAppContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Add(T entity)
            => _dbContext.Set<T>().Add(entity);


        public void Delete(T entity)
            => _dbContext.Set<T>().Remove(entity);


        public IEnumerable<T> GetAll()
           => _dbContext.Set<T>().ToList();


        public T? GetById(int id)
            => _dbContext.Set<T>().Find(id);


        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);
    }
}
