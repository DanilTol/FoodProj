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
            var fromDb = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.id == user.id)?.UserSetes;
            if (fromDb == null) return null;
            var dishes = fromDb.Select(set => set.Dish);

            return UniteDishAndImage.GetDishImagesFromDbAndUnite(Database,dishes);
        }

        public void DeleteOldAndAddNewOrder(DateTime date, int[] dishIds, User user)
        {
            var dishIdsList = dishIds.ToList();
            // get order
            var order = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.id == user.id);
            if (order == null)
            {
                //if it`s a new order
                //TODO: if use user from param it understand like dif context
                var newOrder = new Order {Date = date.Date, User = Database.User.QueryToTable.FirstOrDefault(x => x.id == user.id)};
                Database.Order.Add(newOrder);
                foreach (var dishId in dishIds)
                {
                    Database.UserSet.Add(new UserSet{Dish = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == dishId), Order = newOrder});
                }
            }
            else
            {
                //get order userset
                var oldUserSets = order.UserSetes;
                if (oldUserSets == null) return;
                var oldOrderDishIds = oldUserSets.Select(set => set.Dish.id).ToList();

                foreach (var dishId in dishIds.Where(dishId => oldOrderDishIds.Contains(dishId)))
                {
                    oldOrderDishIds.Remove(dishId);
                    dishIdsList.Remove(dishId);
                }

                foreach (var dishId in oldOrderDishIds)
                {
                    Database.UserSet.Delete(oldUserSets.FirstOrDefault(x => x.Dish.id == dishId));
                }

                foreach (var dishId in dishIdsList)
                {
                    var k = new UserSet
                    {
                        Dish = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == dishId),
                        Order = order
                    };
                    oldUserSets.Add(k);
                    order.UserSetes = oldUserSets;
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