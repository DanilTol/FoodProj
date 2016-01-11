using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
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

        public IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, string email)
        {
            var fromDb = Database.Order.QueryToTable.Where(x => x.Date == date && x.User.EmailAddress == email);
            var allDishes = Mapper.Map<IQueryable<Dish>, IEnumerable<DishModelShortInfo>>(Database.Order.GetDishesFromOrderByDate(date, email));
            return allDishes;
        }

        public void EditOrder(DateTime date, int[] arraInts, string email)
        {
            DeleteOrder(date, email);
            Database.Order.AddSeveralOrders(date, arraInts, email);
            Database.Save();
        }

        public void DeleteOrder(DateTime date, string email)
        {
            Database.Order.DeleteByDate(date,email);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}