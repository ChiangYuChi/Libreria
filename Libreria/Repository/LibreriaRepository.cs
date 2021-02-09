using Libreria.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Libreria.Repository
{
    public class LibreriaRepository
    {
        private readonly DbContext _dbContext;
        public LibreriaRepository()
        {
            _dbContext = new LibreriaDataModel();
        }

        public void Create<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}