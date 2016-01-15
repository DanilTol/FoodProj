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
        
        //public void AddSeveralOrders(DateTime date, int[] dishInts, string email)
        //{
        //    var insert = new Order { Date = date , User = _context.Users.FirstOrDefault(x => x.EmailAddress == email)};
        //    Add(insert);
        //    foreach (var i in dishInts)
        //    {
        //        var dish = _context.Dishes.FirstOrDefault(z => z.ID == i);
        //        _context.UserSets.Add(new UserSet() { Dish = dish, Order = insert });
        //    }
        //}
    }
}