﻿using E_Commerce.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.DAL.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
