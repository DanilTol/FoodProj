using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly EntityContext _context;

        public OrderRepository(EntityContext context)
        {
            _context = context;
        }

        public IQueryable<Order> QueryToTable => _context.Orders;

        public void Add(Order entity)
        {
            _context.Orders.Add(entity);
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
        }

        public void Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}