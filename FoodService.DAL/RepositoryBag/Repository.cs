using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class PublicRepository : IRepositorySecond
    {
        private readonly DbContext _dataContext;

        public PublicRepository(DbContext entitiesContext)
        {

            if (entitiesContext == null)
            {

                throw new ArgumentNullException("entitiesContext");
            }

            _dataContext = entitiesContext;
        }

        public IQueryable<E> GetAll<E>() where E : class
        {
            return _dataContext.Set<E>();
        }

        //public E FindById<E>(int id) where E : CommonClass
        //{
        //    return _dataContext.Set<E>().First(x => x.ID == id);
        //}

        public void Add<E>(E entity) where E : class
        {
            _dataContext.Entry(entity).State = EntityState.Added;
            Save();
        }

        public void Update<E>(E entity) where E : class
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Delete<E>(E entity) where E : class
        {
            _dataContext.Entry(entity).State = EntityState.Deleted;
            Save();
        }

        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

        //public IEnumerable<Dish> GetDishesFromOrderByDate(DateTime date)
        //{
        //    var context = new EntityContext(); 
        //    return (from t in context.Dishes where t.UserSets.All(mem => mem.Order.Date == date) select t);
        //} 
    }
}
