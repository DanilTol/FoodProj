using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.Business.Services.CommonFunc;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class DishService : IDishService
    {
        IUnitOfWork Database { get; }
        //private const string DefaultPathToImage = "../Dish/Common.gif";

        public DishService(IUnitOfWork uow)
        {
            Database = uow;
        }

        //internal static IEnumerable<DishModelShortInfo> GetDishImagesFromDbAndUnite(IUnitOfWork Database, IEnumerable<Dish> dish)
        //{
        //    var allDishes = Mapper.Map<IEnumerable<Dish>, IList<DishModelShortInfo>>(dish);
        //    foreach (var plate in allDishes)
        //    {
        //        var plateImg = Database.DishToImage.QueryToTable.FirstOrDefault(x => x.Dish.ID == plate.ID);
        //        plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
        //    }

        //    return allDishes;
        //}

        public IEnumerable<DishModelShortInfo> FilterDishes(int page, int pageSize, string filter = null)
        {
            IQueryable<Dish> dishesFromDb;
            if (!string.IsNullOrEmpty(filter))
            {
                    dishesFromDb = Database.Dish.QueryToTable.Where(m => m.Name.ToLower().Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.ID)
                        .Skip(page*pageSize)
                        .Take(pageSize);

            }
            else
            {
                dishesFromDb = Database.Dish.QueryToTable
                        .OrderBy(m => m.ID)
                        .Skip(page*pageSize)
                        .Take(pageSize);
            }
           return UniteDishAndImage.GetDishImagesFromDbAndUnite(Database,dishesFromDb);
        }

        public int TotalFilteredDish(string filter = null)
        {
            int totalDishes;
            if (!string.IsNullOrEmpty(filter))
            {
                totalDishes = Database.Dish.QueryToTable
                    .Count(m => m.Name.ToLower().Contains(filter.ToLower().Trim()));
            }
            else
            {
                totalDishes = Database.Dish.QueryToTable.Count();
            }
            return totalDishes;
        }

        
        public void CreateDish(DishModelDetailsInfo dish)
        {
            var toDb = Mapper.Map<DishModelDetailsInfo, Dish>(dish);
            toDb.DishToImages = new List<DishToImage>();
            
            //add images to dish
            foreach (var imgPath in dish.ImagePath)
            {
                var imgRow = new DishToImage
                {
                    Dish = toDb,
                    PathToImageOnServer = imgPath,
                };
                toDb.DishToImages.Add(imgRow);
            }

            //add dish to database
            Database.Dish.Add(toDb);
            Database.Save();
        }

        public DishModelDetailsInfo GetDishById(int id)
        {
            var details = Mapper.Map<Dish, DishModelDetailsInfo>(Database.Dish.QueryToTable.FirstOrDefault(x => x.ID == id));

            var imgList = new List<string>();
            var dishToImageCollection = Database.DishToImage.QueryToTable.Where(x => x.Dish.ID == details.ID);
            foreach (var dishimage in dishToImageCollection)
            {
                imgList.Add(dishimage.PathToImageOnServer);
            }
            details.ImagePath = imgList.Count<1 ? new []{UniteDishAndImage.DefaultPathToImage} : imgList.ToArray() ;
            
            return details;
        }

        public void EditDish(DishModelDetailsInfo dish)
        {
            //var toDb = Mapper.Map<DishModelDetailsInfo, Dish>(dish);

            //TODO: Uploade/Delete images
            ////if images were not uploaded
            //if (dish.ImagePath == null || dish.ImagePath.Length < 1 || string.IsNullOrEmpty(dish.ImagePath[0]))
            //    dish.ImagePath = new[] { DefaultPathToImage };

            ////add images to dish
            //foreach (var imgPath in dish.ImagePath)
            //{
            //    var imgRow = new DishToImage
            //    {
            //        Dish = toDb,
            //        PathToImageOnServer = imgPath
            //    };
            //    toDb.DishToImages.Add(imgRow);
            //}

            var dishDb = Database.Dish.QueryToTable.FirstOrDefault(x => x.ID == dish.ID);
            if (dishDb == null) return;
            dishDb.Name = dish.Name;
            dishDb.Description = dish.Description;
            dishDb.Energy = dish.Energy;
            dishDb.Ingridients = dish.Ingridients;
            dishDb.Price = dish.Price;
            dishDb.Weight = dish.Weight;

            Database.Dish.Update(dishDb);
            Database.Save();
        }

        public void DeleteDish(int id)
        {
            var toDelete = Database.Dish.QueryToTable.FirstOrDefault(x => x.ID == id);
            Database.Dish.Delete(toDelete);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
