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
            //IRepositorySecond a = new PublicRepository(entitiesContext);
            //OrderRepository a = new OrderRepository();
            //var DishToImageRepository = new DishToImageRepository();
            // var k = a.GetDishesFromOrderByDate(date);
            
            //return k.Select( r => new DishModelShortInfo()
            //{
            //    ID = r.ID,
            //    Name = r.Name,
            //    Weight = r.Weight,
            //    Price = r.Price,
            //    ImagePath = DishToImageRepository.FindByDishId(r.ID)
            //});

            //Mapper.CreateMap<Dish, DishModelShortInfo>();
            var allDishes = Mapper.Map<IQueryable<Dish>, IEnumerable<DishModelShortInfo>>(Database.Order.GetDishesFromOrderByDate(date, email));
            return allDishes;


        }

        public void EditOrder(DateTime date, List<string> arraInts, string email)
        {
            DeleteOrder(date, email);
            
            for (int i = 0; i < 5; i++)
            {
                var ai = arraInts[i].Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                if (ai[0] == 0)
                {
                    continue;
                }
                //TODO: find solution
                Database.Order.AddSeveralOrders(date.AddDays(i), ai, email);
            }
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