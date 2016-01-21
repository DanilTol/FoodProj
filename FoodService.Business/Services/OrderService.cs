using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.Business.Services.CommonFunc;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;
using FoodService.Business.Services;

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

        public IEnumerable<OrderInfo> GetOrderListOnWeek(DateTime date)
        {
            var friday = date.AddDays(4);
            var ordersDb = Database.Order.QueryToTable.Where(x => x.Date >= date && x.Date <= friday);
            var orderInfos = new List<OrderInfo>();
            foreach (var order in ordersDb)
            {
                var userDto = Mapper.Map<User, UserDTO>(order.User);
                userDto.Role = order.User.Role.Name;
                var dishShort = Mapper.Map<ICollection<Dish>, IEnumerable<DishModelShortInfo>>(order.Dishes);
            orderInfos.Add(new OrderInfo() {Date = order.Date, Id = order.id, User = userDto, Dishes = dishShort});
            }
            return orderInfos;
        }

        public void DeleteRangeOrders(int[] orderIds)
        {
            foreach (var orderId in orderIds)
            {
                Database.Order.Delete(Database.Order.QueryToTable.FirstOrDefault(x => x.id == orderId));
            }
            Database.Save();
        }

        public void SentMailToChef(DateTime date, string chefMail)
        {
            chefMail = "d.a.tolmachov@gmail.com";
            var orderInfos = GetOrderListOnWeek(date);
            string messageBody = "<table class='table table-striped text-center'><tr><th class='text-center'>Order Id</th><th class='text-center'>Date</th><th class='text-center'>User</th><th class='text-center'>Role</th><th class='text-center'>Dishes</th></tr>";
            foreach (var order in orderInfos)
            {
                messageBody += "<tr><td>" + order.Id + "</td><td>" + order.Date.Date + "</td><td>" + order.User.Name +
                               "</td><td>" + order.User.Role + "</td><td>";
                foreach (var dish in order.Dishes)
                {
                    messageBody += dish.Name + " * " + dish.Count + ";";
                }
            }       
            messageBody += "</td></tr></table>";
            MailService.SentEmail(chefMail, "Orders on " + date.Date, messageBody);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}