using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class OrderDishRepository : IRepository<OrderDish>
    {
        readonly EntityContext _context;

        public OrderDishRepository(EntityContext context)
        {
            this._context = context;
        }

        public IQueryable<OrderDish> QueryToTable => _context.OrderDish;

        public void Add(OrderDish entity)
        {
            _context.OrderDish.Add(entity);
        }

        public void Delete(OrderDish entity)
        {
            _context.OrderDish.Remove(entity);
        }

        public void Update(OrderDish entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        
    }
}
