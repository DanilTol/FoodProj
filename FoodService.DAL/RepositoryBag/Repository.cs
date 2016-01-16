using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class PublicRepository : IRepositorySecond
    {
        private readonly EntityContext _dataContext;

        public PublicRepository(EntityContext entitiesContext)
        {
            _dataContext = entitiesContext;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dataContext.Set<T>();
        }
        
        public void Add<T>(T entity) where T : class
        {
            _dataContext.Entry(entity).State = EntityState.Added;
        }

        public void Update<T>(T entity) where T : class
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
