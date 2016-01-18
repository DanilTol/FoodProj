using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class DayDishSetRepository : IRepository<DayDishSet>
    {
        readonly EntityContext _context;

        public DayDishSetRepository(EntityContext context)
        {
            this._context = context;
        }

        public IQueryable<DayDishSet> QueryToTable => _context.DayDishSet;

        public void Add(DayDishSet entity)
        {
            _context.DayDishSet.Add(entity);
        }

        public void Delete(DayDishSet entity)
        {
            _context.DayDishSet.Remove(entity);
        }

        public void Update(DayDishSet entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        
    }
}
