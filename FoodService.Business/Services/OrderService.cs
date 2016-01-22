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

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, User user)
        {
            var fromDb =
                Database.OrderDishes.QueryToTable.Where(x => x.Order.Date == date.Date && x.Order.User.id == user.id);
            var dishesInfo =
                UniteDishAndImage.GetDishImagesFromDbAndUnite(Database, fromDb.Select(x => x.Dish)).ToList();
            var dishCount = fromDb.Select(x => x.Count).ToList();
            for (int i = 0; i < dishesInfo.Count; i++)
            {
                dishesInfo[i].Count = dishCount[i];
            }
            return dishesInfo;
        }

        public void UpdateOrder(DateTime date, int[] dishIds, int[] dishNum, User user)
        {
            // get order
            var order = Database.Order.QueryToTable.FirstOrDefault(x => x.Date == date.Date && x.User.id == user.id);
            if (order == null && dishIds.Length > 0)
            {
                //if it`s a new order
                var newOrder = new Order
                {
                    Date = date.Date,
                    User = Database.User.QueryToTable.FirstOrDefault(x => x.id == user.id)
                };
                var orderDishList = new List<OrderDish>();
                var dishIdAndCounts = dishIds.Zip(dishNum, (id, count) => new {dishId = id, dishCount = count});
                foreach (var idAndCount in dishIdAndCounts)
                {
                    var temp = new OrderDish()
                    {
                        Dish = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == idAndCount.dishId),
                        Order = newOrder,
                        Count = idAndCount.dishCount
                    };
                    orderDishList.Add(temp);
                    Database.OrderDishes.Add(temp);
                }
                newOrder.OrderDishes = orderDishList;
                Database.Order.Add(newOrder);
            }
            else
            {
                if (dishIds.Length > 0)
                {
                    var dishIdAndCounts = dishIds.Zip(dishNum, (id, count) => new {dishId = id, dishCount = count});
                    var addOrderDishes =
                        dishIdAndCounts.Select(
                            dishinfo => new KeyValuePair<int, int>(dishinfo.dishId, dishinfo.dishCount)).ToList();

                    //getting all dishes from order(dishes potential for delete)
                    var deleteOrderDishes = order.OrderDishes.Select(x => x.Dish).ToList();

                    //fill equal list
                    var equalDishesInOrderDish =
                        (from newDish in addOrderDishes
                            from oldDish in deleteOrderDishes
                            where oldDish.id == newDish.Key
                            select newDish).ToList();

                    // remove equals(dishes that will leave in DB) from adding and removing lista
                    foreach (var e in equalDishesInOrderDish)
                    {
                        deleteOrderDishes.RemoveAll(x => x.id == e.Key);
                        addOrderDishes.Remove(e);
                    }

                    // delete old orderDishes(unequal)
                    foreach (var old in deleteOrderDishes)
                    {
                        Database.OrderDishes.Delete(
                            Database.OrderDishes.QueryToTable.FirstOrDefault(
                                x => x.Dish.id == old.id && x.Order.id == order.id));
                    }

                    //Update count
                    foreach (var e in equalDishesInOrderDish)
                    {
                        var countCheck = order.OrderDishes.FirstOrDefault(x => x.Dish.id == e.Key);
                        countCheck.Count = e.Value;
                        Database.OrderDishes.Update(countCheck);
                    }

                    //add new
                    foreach (var orderDish in addOrderDishes)
                    {
                        Database.OrderDishes.Add(new OrderDish
                        {
                            Count = orderDish.Value,
                            Order = order,
                            Dish = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == orderDish.Key)
                        });
                    }
                    order.Checked = false;
                    Database.Order.Update(order);
                }
                else
                {
                    //if list of ordering dishes is empty
                    var orderDishesToDelete = Database.OrderDishes.QueryToTable.Where(x => x.Order.id == order.id);
                    foreach (var orderDish in orderDishesToDelete)
                    {
                        Database.OrderDishes.Delete(orderDish);
                    }
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
                var dishShort =
                    Mapper.Map<IEnumerable<Dish>, List<DishModelShortInfo>>(order.OrderDishes.Select(dish => dish.Dish));
                foreach (var dish in dishShort)
                {
                    dish.Count = order.OrderDishes.FirstOrDefault(x => x.Dish.id == dish.ID).Count;
                }
                orderInfos.Add(new OrderInfo {Date = order.Date, Id = order.id, User = userDto, Dishes = dishShort, Checked = order.Checked});
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
            //chefMail = "d.a.tolmachov@gmail.com";
            var orderInfos = GetOrderListOnWeek(date);
            foreach (var order in orderInfos)
            {
                var orderDb = Database.Order.QueryToTable.FirstOrDefault(x => x.id == order.Id);
                orderDb.Checked = true;
                Database.Order.Update(orderDb);
            }
            string messageBody =
                "<table style='border: 1px solid black'><tr><th>Order Id</th><th>Date</th><th>User</th><th>Role</th><th>Dishes</th></tr>";
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

        public int NumberOfUnchecked()
        {
            return Database.Order.QueryToTable.Count(x => x.Checked == false);
        }

        public IEnumerable<OrderInfo> NotCheckedOrders()
        {
            var ordersDb = Database.Order.QueryToTable.Where(x => x.Checked == false);
            var orderInfos = new List<OrderInfo>();
            foreach (var order in ordersDb)
            {
                var userDto = Mapper.Map<User, UserDTO>(order.User);
                userDto.Role = order.User.Role.Name;
                var dishShort =
                    Mapper.Map<IEnumerable<Dish>, List<DishModelShortInfo>>(order.OrderDishes.Select(dish => dish.Dish));
                foreach (var dish in dishShort)
                {
                    dish.Count = order.OrderDishes.FirstOrDefault(x => x.Dish.id == dish.ID).Count;
                }
                orderInfos.Add(new OrderInfo { Date = order.Date, Id = order.id, User = userDto, Dishes = dishShort , Checked = order.Checked});
            }
            return orderInfos;
        }

    public void Dispose()
        {
            Database.Dispose();
        }
    }
}