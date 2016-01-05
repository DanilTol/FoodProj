using System;
using System.Collections.Generic;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class DaySetService : IDaySetService
    {
        IUnitOfWork Database { get; set; }

        public DaySetService(IUnitOfWork uow)
        {
            Database = uow;
        }



        public IEnumerable<DishModelShortInfo> GetDayInfo(DateTime dateTime)
        {
            var fromDb = Database.DayDish.GetAllDishSetsOnDay(dateTime);
            List<DishModelShortInfo> result = new List<DishModelShortInfo>();
            foreach (var s in fromDb)
            {
                string pathImage = Database.DishToImage.FindByDishId(s.Dish.ID);
                result.Add(new DishModelShortInfo()
                {
                    ID = s.Dish.ID,
                    Name = s.Dish.Name,
                    Weight = s.Dish.Weight,
                    Price = s.Dish.Price,
                    ImagePath = pathImage
                });
            }

            return result;
        }

        public void DeleteAndEditWeekDishSet(DateTime date, int[] dishIds)
        {
            Database.DayDish.DeleteByDate(date);
            foreach (var i in dishIds)
            {
                Database.DayDish.Add(new DayDishSet() {Date = date, Dish = Database.Dish.FindById(i), DishId = i});
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}