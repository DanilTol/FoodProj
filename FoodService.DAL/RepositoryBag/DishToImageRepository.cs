using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.DAL.RepositoryBag
{
    public class DishToImageRepository : IRepository<DishToImage>
    {
        readonly EntityContext _context;

        public DishToImageRepository()
        {
            _context = new EntityContext();
        }
        
        public DishToImageRepository(EntityContext entityContext)
        {
            _context = entityContext;
        }

        public IQueryable<DishToImage> QueryToTable => _context.DishToImage;

        public void Add(DishToImage entity)
        {
            _context.DishToImage.Add(entity);
        }

        public void Delete(DishToImage entity)
        {
            _context.DishToImage.Remove(entity);
        }

        public void Update(DishToImage dish)
        {
            _context.Entry(dish).State = EntityState.Modified;
        }
        
    }
}
