using System;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.RepositoryBag
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly EntityContext _context;

        public OrderRepository()
        {
            _context = new EntityContext();
        }

        public OrderRepository(EntityContext context)
        {
            _context = context;
        }


        public IQueryable<Order> QueryToTable
        {
            get { return _context.Orders; }
        }
        public void Add(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = (from r in _context.Orders where id == r.ID select r).FirstOrDefault();
            //var result2 = _context.Orders.FirstOrDefault(x => x.ID == id);
            _context.Orders.Remove(result);
            _context.SaveChanges();

        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order FindById(int id)
        {
            var result = (from r in _context.Orders where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public IQueryable<Order> Find(Func<Order, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Dish> GetDishesFromOrderByDate(DateTime date, string email)
        {
            var a = (from s in _context.Dishes
                     from e in s.UserSets
                     where e.Order.Date == date && e.Order.User.EmailAddress == email
                     select s);
            return a;


            // var context = new EntityContext();
            //return (from t in context.Dishes where t.UserSets.All(mem => mem.Order.Date == date && mem.Order.ID == id) select t);
        }

        public void DeleteByDate(DateTime date, string email)
        {
            var rightdate = date.AddDays(5);
            var leftdate = date.AddDays(-1);
            _context.Orders.RemoveRange(_context.Orders.Where(x => (x.Date > leftdate) && (x.Date < rightdate)&& x.User.EmailAddress==email));
        }

        public void AddSeveralOrders(DateTime date, int[] dishInts, string email)
        {

            var insert = new Order { Date = date , User = _context.Users.FirstOrDefault(x => x.EmailAddress == email)};
            Add(insert);
            foreach (var i in dishInts)
            {
                var dish = (from z in _context.Dishes where z.ID == i select z).FirstOrDefault();
                _context.UserSets.Add(new UserSet() { Dish = dish, Order = insert });
                SaveData();
            }
        }

        public void SaveData()
        {
            _context.SaveChanges();
        }

    }
}