using System.Linq;

namespace FoodService.DAL.Interfaces
{
    public interface IRepositorySecond
    {
        IQueryable<TE> GetAll<TE>() where TE : class;
        bool Add<TE>(TE entity) where TE : class;
        bool Update<TE>(TE entity) where TE : class;
        bool Delete<TE>(TE entity) where TE : class;
        bool Save();
    }
}