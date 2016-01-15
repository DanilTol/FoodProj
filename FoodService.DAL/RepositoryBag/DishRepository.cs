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

        public IQueryable<Dish> QueryToTable => _context.Dishes;
        
        public void Add(Dish entity)
        {
            _context.Dishes.Add(entity);
        }

        public void Delete(Dish entity)
        {
            _context.Dishes.Remove(entity);
        }

        public void Update(Dish dish)
        {
            _context.Entry(dish).State = EntityState.Modified;

            //var result = (from r in _context.Dishes where dish.ID == r.ID select r).FirstOrDefault();
            //if (result != null)
            //{
            //    result.Name = dish.Name;
            //    result.Description = dish.Description;
            //    result.Energy = dish.Energy;
            //    result.Ingridients = dish.Ingridients;
            //    result.Price = dish.Price;
            //    result.Weight = dish.Weight;
            //    _context.Entry(result).State = EntityState.Modified;
            //}
        }

       
    }
}
