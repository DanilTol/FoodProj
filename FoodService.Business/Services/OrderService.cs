using System;
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
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }
        //private const string DefaultPathToImage = "../Dish/Common.gif";

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, string email)
        {
            var fromDb = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.EmailAddress == email)?.UserSetes;
            if (fromDb == null) return null;
            var dishes = fromDb.Select(set => set.Dish);

            return UniteDishAndImage.GetDishImagesFromDbAndUnite(Database,dishes);

            //var allDishes = Mapper.Map<IEnumerable<Dish>, IList<DishModelShortInfo>>(dishes);
            //foreach (var plate in allDishes)
            //{
            //    var plateImg = Database.DishToImage.QueryToTable.FirstOrDefault(x => x.Dish.ID == plate.ID);
            //    plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
            //}
            //return allDishes;
        }

        public void EditOrder(DateTime date, int[] arraInts, string email)
        {
            DeleteOrder(date, email);
            Database.Order.AddSeveralOrders(date.Date, arraInts, email);

            //var newOrder = new Order { Date = date, User = Database.User.QueryToTable.FirstOrDefault(x => x.EmailAddress == email) };
            //foreach (var i in arraInts)
            //{
            //    var dish = Database.Dish.QueryToTable.FirstOrDefault(z => z.ID == i);
            //    Da.UserSets.Add(new UserSet() { Dish = dish, Order = newOrder });
            //}
            Database.Save();
        }

        public void DeleteOrder(DateTime date, string email)
        {
            Database.Order.DeleteByDate(date.Date,email);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}