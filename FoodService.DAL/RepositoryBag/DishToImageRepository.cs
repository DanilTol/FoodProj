using System;
using System.Data.Entity;
using System.Linq;
using FoodService.DAL.Entity;

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
            //_context.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = (from r in _context.Dishes where id == r.ID select r).FirstOrDefault();
            _context.Dishes.Remove(result);
            _context.SaveChanges();
        }

        public void Update(DishToImage dish)
        {
            var result = (from r in _context.DishToImage where dish.ID == r.ID select r).FirstOrDefault();
            if (result != null)
            {
                result.PathToImageOnServer = dish.PathToImageOnServer;
                result.Dish = dish.Dish;
                _context.Entry(result).State = EntityState.Modified;
            }
            
            _context.SaveChanges();
        }

        public DishToImage FindById(int id)
        {
            var result = (from r in _context.DishToImage where r.ID == id select r).FirstOrDefault();
            return result;
        }

        public IQueryable<DishToImage> Find(Func<DishToImage, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public string FindByDishId(int id)
        {
        //    SELECT[PathToImageOnServer],
        //            [Dish_ID]
        //    FROM(
        //            SELECT
        //                     [PathToImageOnServer]
        //                     , [Dish_ID],
        //                     ROW_NUMBER() OVER(PARTITION BY Dish_ID ORDER BY ID DESC) as rn
        //            FROM DishToImages
        //            ) a
        //    WHERE rn = 1
            //IQueryable<DishToImage> result = null;
            //context.Database.ExecuteSqlCommand(
            //    "SELECT [ID],[PathToImageOnServer],[Dish_ID] " +
            //    "FROM( SELECT [ID],[PathToImageOnServer] ,[Dish_ID], ROW_NUMBER() OVER(PARTITION BY Dish_ID ORDER BY ID DESC) as rn " +
            //    "FROM DishToImages ) a WHERE rn = 1 AND Dish_ID=" + id,
            //    result);

            //var result = (from r in context.DishToImage where r.Dish.ID == id select r).FirstOrDefault();


            var result = (from r in _context.DishToImage where r.Dish.ID == id select r).FirstOrDefault()?.PathToImageOnServer;
            
            return result;
        }

        public string[] FindByDishIdAllImages(int id)
        {
            var serverAnswer = (from r in _context.DishToImage where r.Dish.ID == id select r);
            //var result = from s in serverAnswer select s.PathToImageOnServer;
            var k = serverAnswer.Select(r => (r.PathToImageOnServer));
            return k.ToArray();
            //return result;
        }
    }
}
