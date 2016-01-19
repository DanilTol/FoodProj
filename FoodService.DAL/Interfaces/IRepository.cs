using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> QueryToTable { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
