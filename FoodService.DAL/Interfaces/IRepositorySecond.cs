using System.Collections.Generic;
using System.Linq;

namespace FoodService.DAL.Interfaces
{
    public interface IRepositorySecond
    {
        //void Add<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
        //void SaveChanges();


        IQueryable<E> GetAll<E>() where E : class;
        //E FindById<E>(int id) where E : class;
        void Add<E>(E entity) where E : class;
        void Update<E>(E entity) where E : class;
        void Delete<E>(E entity) where E : class;
        bool Save();


    }
}