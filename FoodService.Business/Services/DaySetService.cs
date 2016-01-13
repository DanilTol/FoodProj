using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mail;
using FoodService.Business.DTO;
using FoodService.Business.ServiceInterfaces;
using FoodService.DAL.Entity;
using FoodService.DAL.Interfaces;

namespace FoodService.Business.Services
{
    public class DaySetService : IDaySetService
    {
        IUnitOfWork Database { get; set; }
        private const string DefaultPathToImage = "../Dish/Common.gif";

        public DaySetService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DishModelShortInfo> GetDayInfo(DateTime dateTime)
        {
            //var fromDb = Database.DayDish.GetAllDishSetsOnDay(dateTime.Date);
            var fromDb = Database.DayDish.QueryToTable.Where(x => x.Date == dateTime.Date);
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
                    ImagePath = pathImage ?? DefaultPathToImage
                });
            }

            return result;
        }

        public IEnumerable<DishModelShortInfo> Filter(DateTime dateTime, string filter)
        {
            IQueryable<DayDishSet> fromDb;
            if (String.IsNullOrEmpty(filter))
            {
                //fromDb = Database.DayDish.GetAllDishSetsOnDay(dateTime.Date).OrderBy(m => m.ID);
                fromDb = Database.DayDish.QueryToTable.Where(m => m.Date == dateTime.Date).OrderBy(m => m.ID);
            }
            else
            {
                fromDb = Database.DayDish.QueryToTable.Where(m => m.Date == dateTime.Date)
                    .Where(m => m.Dish.Name.ToLower().Contains(filter.ToLower().Trim()))
                    .OrderBy(m => m.ID);

                //fromDb = Database.DayDish.GetAllDishSetsOnDay(dateTime.Date)
                //   .Where(m => m.Dish.Name.ToLower().Contains(filter.ToLower().Trim()))
                //   .OrderBy(m => m.ID);
            }
            var result = new List<DishModelShortInfo>();
            foreach (var s in fromDb)
            {
                string pathImage = Database.DishToImage.FindByDishId(s.Dish.ID);
                result.Add(new DishModelShortInfo()
                {
                    ID = s.Dish.ID,
                    Name = s.Dish.Name,
                    Weight = s.Dish.Weight,
                    Price = s.Dish.Price,
                    ImagePath = pathImage ?? DefaultPathToImage
                });
            }
            return result;
        }


        public void DeleteAndEditDayDishSet(DateTime date, int[] dishIds)
        {
            var dishIdsList = dishIds.ToList();
            var menuDelete = Database.DayDish.QueryToTable.Where(x => x.Date == date.Date).ToList();

            var equalList = menuDelete.SelectMany(daymenu => dishIdsList.Where(dishId => daymenu.Dish.ID == dishId)).ToList();

            foreach (var eq in equalList)
            {
                dishIdsList.Remove(eq);
                menuDelete.RemoveAll(x => x.Dish.ID == eq);
            }

            foreach (var dish in menuDelete)
            {
                //Database.DayDish.Delete(dish.ID);
                Database.DayDish.Delete(dish);
            }

            foreach (var i in dishIdsList)
            {
                Database.DayDish.Add(i, date);
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}