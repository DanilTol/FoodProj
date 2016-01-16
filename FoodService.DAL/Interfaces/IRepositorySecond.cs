using System.Linq;

namespace FoodService.DAL.Interfaces
{
    public interface IRepositorySecond
    {
        IQueryable<T> GetAll<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool Save();
    }
}