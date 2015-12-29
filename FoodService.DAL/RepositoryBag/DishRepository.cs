using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;

namespace FoodService.DAL.RepositoryBag
{
    public class DishRepository : IRepository<Dish>
    {
        readonly EntityContext _context;

        public DishRepository(EntityContext context)
        {
            this._context = context;
        }

        public DishRepository()
        {
            _context = new EntityContext();
        }

        public IQueryable<Dish> GetAll
        {
            get { return _context.Dishes; }
        }

        //private void Add(Dish entity, string imgpath)
        //{
        //    context.Dishes.Add(entity);
        //    //context.DishToImage.Add(new DishToImage { IdDish = entity.ID, PathToImageOnServer = imgpath });
        //    context.SaveChanges();
        //}

        public void Add(Dish entity)
        {
            _context.Dishes.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = (from r in _context.Dishes where id == r.ID select r).FirstOrDefault();
            _context.Dishes.Remove(result);
            _context.SaveChanges();
        }

        public void Update(Dish dish)
        {
            var result = (from r in _context.Dishes where dish.ID == r.ID select r).FirstOrDefault();
            if (result != null)
            {
                result.Name = dish.Name;
                result.Description = dish.Description;
                result.Energy = dish.Energy;
                result.Ingridients = dish.Ingridients;
                result.Price = dish.Price;
                result.Weight = dish.Weight;
                _context.Entry(result).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }

        public Dish FindById(int id)
        {
            var result = (from r in _context.Dishes where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public IQueryable<Dish> Find(Func<Dish, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
