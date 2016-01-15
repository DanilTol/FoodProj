using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services.CommonFunc
{
    public class UniteDishAndImage
    {
        public static string DefaultPathToImage = "../Dish/Common.gif";

        internal static IEnumerable<DishModelShortInfo> GetDishImagesFromDbAndUnite(IUnitOfWork unitOfWork,IEnumerable<Dish> dish)
        {
            var allDishes = Mapper.Map<IEnumerable<Dish>, IEnumerable<DishModelShortInfo>>(dish);
            foreach (var plate in allDishes)
            {
                var plateImg = unitOfWork.DishToImage.QueryToTable.FirstOrDefault(x => x.Dish.ID == plate.ID);
                plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
            }
            return allDishes;
        }
    }
}