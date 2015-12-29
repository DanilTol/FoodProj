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



        public IEnumerable<DishModelShortInfo> GetWeekInfo(DateTime dateTime)
        {
            //WeekDishSetRepository weekDishRepository = new WeekDishSetRepository();
            //DishToImageRepository imageRepository = new DishToImageRepository();


            //var fromDB = weekDishRepository.GetAllDishSetsOnDay(dateTime);
            var fromDB = Database.WeekDish.GetAllDishSetsOnDay(dateTime);
            List<DishModelShortInfo> result = new List<DishModelShortInfo>();
            //var pathimage = imageRepository.FindById(r.Dish.ID).PathToImageOnServer;
            foreach (var s in fromDB)
            {
                //string pathImage = imageRepository.FindByDishId(s.Dish.ID);
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








            //return ((IWeekDishSet<WeekDishSet>) weekDishRepository).GetAllDishSetsOnWeek(dateTime).Select(r =>
            //    new DishModelShortInfo(r.Dish.ID, r.Dish.Name, r.Dish.Weight, r.Dish.Price, imageRepository.FindById(r.Dish.ID).PathToImageOnServer));
            
        }

        public void DeleteAndEditWeekDishSet(DateTime date, int[] dishIds)
        {

            //var weekRep = new WeekDishSetRepository();
            //var dishRep = new DishRepository();

            //weekRep.DeleteByDate(date);

            //foreach (WeekDishSet set in dishIds.Select(t => new WeekDishSet
            //{
            //    Dish = dishRep.FindById(t),
            //    Date = date,
            //    DishId = t
            //}))
            //{
            //    weekRep.Add(set);
            //}

            Database.WeekDish.DeleteByDate(date);
            foreach (var i in dishIds)
            {
                Database.WeekDish.Add(new WeekDishSet() {Date = date, Dish = Database.Dish.FindById(i), DishId = i});
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}