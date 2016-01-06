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

        IQueryable<T> QueryToTable { get; }
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
        T FindById(int id);
    }
}
