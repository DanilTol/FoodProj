using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mail;
using AutoMapper;
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
        
        public IEnumerable<DishModelShortInfo> Filter(DateTime dateTime, string filter)
        {
            IQueryable<DayDishSet> fromDb;
            if (string.IsNullOrEmpty(filter))
            {
                fromDb = Database.DayDish.QueryToTable.Where(m => m.Date == dateTime.Date).OrderBy(m => m.ID);
            }
            else
            {
                fromDb = Database.DayDish.QueryToTable.Where(m => m.Date == dateTime.Date)
                    .Where(m => m.Dish.Name.ToLower().Contains(filter.ToLower().Trim()))
                    .OrderBy(m => m.ID);
            }


            DishService dishService = new DishService(Database);

            return dishService.GetImgAndDishTogether(fromDb.Select(set => set.Dish).ToList());

            //var dishFromDb = fromDb.Select(set => set.Dish).ToList();
            //var allDishes = Mapper.Map<IList<Dish>, IList<DishModelShortInfo>>(fromDb.Select(set => set.Dish).ToList());
            //foreach (var plate in allDishes)
            //{
            //    var plateImg = Database.DishToImage.QueryToTable.FirstOrDefault(x => x.Dish.ID == plate.ID);
            //    plate.ImagePath = plateImg != null ? plateImg.PathToImageOnServer : DefaultPathToImage;
            //}
            //return allDishes;
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