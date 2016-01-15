using System;
using System.Collections.Generic;
using System.Linq;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.Business.Services.CommonFunc;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

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
        }

        public void DeleteOldAndAddNewOrder(DateTime date, int[] dishIds, int userId)
        {
            var dishIdsList = dishIds.ToList();
            // get day set by date
            var oldUserSets = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.ID == userId)?.UserSetes;
            if (oldUserSets == null) return;
            var oldOrderDishes = oldUserSets.Select(set => set.Dish.ID).ToList();

            var equalList = oldUserSets.SelectMany(oldUserSet => dishIdsList.Where(dishId => oldUserSet.Dish.ID == dishId)).ToList();

            foreach (var eq in equalList)
            {
                dishIdsList.Remove(eq);

                //oldOrder.RemoveAll(x => x.Dish.ID == eq);
            }

            
            ////delete old dishes from menu 
            //foreach (var oldUserSet in oldUserSets)
            //{
            //    Database..Delete(dish);
            //}
            ////add new dishes
            //foreach (var i in dishIdsList)
            //{
            //    Database.DayDish.Add(i, date);
            //}
            //Database.Save();


            //DeleteOrder(date, email);
            //Database.Order.AddSeveralOrders(date.Date, dishIds, email);

            //var newOrder = new Order { Date = date, User = Database.User.QueryToTable.FirstOrDefault(x => x.EmailAddress == email) };
            //foreach (var i in arraInts)
            //{
            //    var dish = Database.Dish.QueryToTable.FirstOrDefault(z => z.ID == i);
            //    Da.UserSets.Add(new UserSet() { Dish = dish, Order = newOrder });
            //}
            Database.Save();
        }

        public void DeleteOrder(DateTime date, int userId)
        {
            Database.Order.Delete(Database.Order.QueryToTable.FirstOrDefault(x => x.User.ID == userId && x.Date == date.Date));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}