using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class DishImageRepository : IRepository<DishImage>
    {
        readonly EntityContext _context;

        public DishImageRepository(EntityContext entityContext)
        {
            _context = entityContext;
        }

        public IQueryable<DishImage> QueryToTable => _context.DishImage;

        public void Add(DishImage entity)
        {
            _context.DishImage.Add(entity);
        }

        public void Delete(DishImage entity)
        {
            _context.DishImage.Remove(entity);
        }

        public void Update(DishImage dish)
        {
            _context.Entry(dish).State = EntityState.Modified;
        }
        
    }
}
