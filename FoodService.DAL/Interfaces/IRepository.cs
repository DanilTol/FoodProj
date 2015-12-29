using System;
using System.Collections.Generic;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL
{
    public interface IRepository<T> where T: CommonClass
    {
        //T GetById(int id);
        //bool Add<TE>(TE entity);
        //bool Update<TE>(TE entity);
        //bool Delete<TE>(TE entity);

        //void Create(T entity);
        //IQueryable<T> GetAll(T entity);
        //void Update(T entity);
        //void Delete(T entity);

        IQueryable<T> GetAll { get; }
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        T FindById(int id);
        IQueryable<T> Find(Func<T, Boolean> predicate);

    }
}
