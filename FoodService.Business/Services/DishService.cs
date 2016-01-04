using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class DishService : IDishService
    {
        IUnitOfWork Database { get; }
        private const string DefaultPathToImage = "../../Dish/Common.gif";

        public DishService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DishModelShortInfo> GetAllDishes()
        {
            var allDishes = Mapper.Map<IQueryable<Dish>, IEnumerable<DishModelShortInfo>>(Database.Dish.GetAll);
            foreach (var plate in allDishes)
            {
                var plateImg = Database.DishToImage.GetAll.FirstOrDefault(x => x.Dish.ID == plate.ID);
                plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
            }
            
            return allDishes;
        }

        public IEnumerable<DishModelShortInfo> FilterDishes(int page, int pageSize, string filter = null)
        {
            List<Dish> shortDish = new List<Dish>();
            IEnumerable<DishModelShortInfo> allDishes;
            if (!string.IsNullOrEmpty(filter))
            {
                    allDishes = Mapper.Map<IQueryable<Dish>, IEnumerable<DishModelShortInfo>>(
                    Database.Dish.GetAll.Where(m => m.Name.ToLower().Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.ID)
                        .Skip(page*pageSize)
                        .Take(pageSize));

            }
            else
            {
                allDishes = Mapper.Map<IQueryable<Dish>, IEnumerable<DishModelShortInfo>>(
                    Database.Dish.GetAll
                        .OrderBy(m => m.ID)
                        .Skip(page*pageSize)
                        .Take(pageSize));
            }
            foreach (var plate in allDishes)
            {
                var plateImg = Database.DishToImage.GetAll.FirstOrDefault(x => x.Dish.ID == plate.ID);
                plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
            }


            return allDishes;
        }

        public int TotalFilteredDish(string filter = null)
        {
            int totalDishes = 0;
            if (!string.IsNullOrEmpty(filter))
            {
                totalDishes = Database.Dish.GetAll
                    .Count(m => m.Name.ToLower()
                    .Contains(filter.ToLower().Trim()));
            }
            else
            {
                totalDishes = Database.Dish.GetAll.Count();
            }
            return totalDishes;
        }

        
        public void CreateDish(DishModelDetailsInfo dish)
        {
            var toDb = Mapper.Map<DishModelDetailsInfo, Dish>(dish);

            //if images were not uploaded
            if (dish.ImagePath == null || dish.ImagePath.Length < 1 || string.IsNullOrEmpty(dish.ImagePath[0]))
                dish.ImagePath = new[] { DefaultPathToImage };

            //add images to dish
            //foreach (var imgPath in dish.ImagePath)
            //{
            //    var imgRow = new DishToImage
            //    {
            //        Dish = toDb,
            //        PathToImageOnServer = imgPath
            //    };
            //    toDb.DishToImages.Add(imgRow);
            //}

            //add dish to database
            Database.Dish.Add(toDb);
            Database.Save();
        }

        public DishModelDetailsInfo GetDishById(int id)
        {
            DishModelDetailsInfo details = Mapper.Map<Dish, DishModelDetailsInfo>(Database.Dish.FindById(id));

            List<string> imgList = new List<string>();
            var dishToImageCollection = Database.DishToImage.GetAll.Where(x => x.Dish.ID == details.ID);
            foreach (var dishimage in dishToImageCollection)
            {
                imgList.Add(dishimage.PathToImageOnServer);
            }
            details.ImagePath = imgList.Count<1 ? new []{DefaultPathToImage} : imgList.ToArray() ;
            
            return details;
        }

        public void EditDish(DishModelDetailsInfo dish)
        {
            var toDb = Mapper.Map<DishModelDetailsInfo, Dish>(dish);

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

            Database.Dish.Update(toDb);
        }

        public void DeleteDish(int id)
        {
            Database.Dish.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
