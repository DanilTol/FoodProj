using System;
using System.Collections.Generic;
using System.Linq;
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

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, User user)
        {
            var fromDb = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.id == user.id)?.Dishes;
            return fromDb == null ? null : UniteDishAndImage.GetDishImagesFromDbAndUnite(Database,fromDb);
        }

        public void DeleteOldAndAddNewOrder(DateTime date, int[] dishIds, User user)
        {
            // get order
            var order = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.id == user.id);
            if (order == null && dishIds.Length>0)
            {
                //if it`s a new order
                //TODO: if use user from param it understand like dif context
                var dishMenu = dishIds.Select(dishId => Database.Dish.QueryToTable.FirstOrDefault(x => x.id == dishId)).ToList();
                var newOrder = new Order {Date = date.Date, User = Database.User.QueryToTable.FirstOrDefault(x => x.id == user.id), Dishes = dishMenu};
                Database.Order.Add(newOrder);
                
            }
            else
            {
                if (dishIds.Length > 0)
                {
                    var dishMenu =
                        dishIds.Select(dishId => Database.Dish.QueryToTable.FirstOrDefault(x => x.id == dishId))
                            .ToList();
                    order.Dishes = dishMenu;
                    Database.Order.Update(order);
                }
                else
                {
                    Database.Order.Delete(order);
                }
            }
            Database.Save();
        }
        
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}