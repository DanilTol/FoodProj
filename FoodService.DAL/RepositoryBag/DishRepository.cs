using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class DishRepository : IRepository<Dish>
    {
        readonly EntityContext _context;

        public DishRepository(EntityContext context)
        {
            this._context = context;
        }

        public IQueryable<Dish> QueryToTable => _context.Dish;
        
        public void Add(Dish entity)
        {
            _context.Dish.Add(entity);
        }

        public void Delete(Dish entity)
        {
            _context.Dish.Remove(entity);
        }

        public void Update(Dish dish)
        {
            _context.Entry(dish).State = EntityState.Modified;
        }
    }
}
