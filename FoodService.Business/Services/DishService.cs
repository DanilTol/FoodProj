﻿using System;
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
    public class DishService : IDishService
    {
        IUnitOfWork Database { get; }

        public DishService(IUnitOfWork uow)
        {
            Database = uow;
        }
        
        public IEnumerable<DishModelShortInfo> FilterDishes(int page, int pageSize, string filter = null)
        {
            IQueryable<Dish> dishesFromDb;
            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "-random")
                {
                    var randNum = new Random();
                    var skip = randNum.Next(1, TotalFilteredDish("") - pageSize);
                    dishesFromDb = Database.Dish.QueryToTable
                        .OrderBy(m => m.id)
                        .Skip(skip)
                        .Take(pageSize);
                }
                else
                {
                    dishesFromDb = Database.Dish.QueryToTable.Where(
                        m => m.Name.ToLower().Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.id)
                        .Skip(page*pageSize)
                        .Take(pageSize);
                }
            }
            else
            {
                    dishesFromDb = Database.Dish.QueryToTable
                        .OrderBy(m => m.id)
                        .Skip(page*pageSize)
                        .Take(pageSize);
                
            }
            return UniteDishAndImage.GetDishImagesFromDbAndUnite(Database,dishesFromDb);
        }

        public int TotalFilteredDish(string filter = null)
        {
            int totalDishes;
            if (!string.IsNullOrEmpty(filter))
            {
                totalDishes = Database.Dish.QueryToTable
                    .Count(m => m.Name.ToLower().Contains(filter.ToLower().Trim()));
            }
            else
            {
                totalDishes = Database.Dish.QueryToTable.Count();
            }
            return totalDishes;
        }

        
        public void CreateDish(DishModelDetailsInfo dish)
        {
            var toDb = Mapper.Map<DishModelDetailsInfo, Dish>(dish);
            toDb.Images = new List<DishImage>();
            
            //add images to dish
            foreach (var imgPath in dish.ImagePath)
            {
                var imgRow = new DishImage
                {
                    Dish = toDb,
                    Path = imgPath,
                };
                toDb.Images.Add(imgRow);
            }

            //add dish to database
            Database.Dish.Add(toDb);
            Database.Save();
        }

        public DishModelDetailsInfo GetDishById(int id)
        {
            var details = Mapper.Map<Dish, DishModelDetailsInfo>(Database.Dish.QueryToTable.FirstOrDefault(x => x.id == id));

            var imgList = new List<string>();
            var dishToImageCollection = Database.DishImage.QueryToTable.Where(x => x.Dish.id == details.ID);
            foreach (var dishimage in dishToImageCollection)
            {
                imgList.Add(dishimage.Path);
            }
            details.ImagePath = imgList.Count<1 ? new []{UniteDishAndImage.DefaultPathToImage} : imgList.ToArray() ;
            
            return details;
        }

        public void EditDish(DishModelDetailsInfo dish)
        {
           
            var dishDb = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == dish.ID);
            if (dishDb == null) return;
            dishDb.Name = dish.Name;
            dishDb.Description = dish.Description;
            dishDb.Energy = dish.Energy;
            dishDb.Ingridients = dish.Ingridients;
            dishDb.Price = dish.Price;
            dishDb.Weight = dish.Weight;

            var oldImages = Database.DishImage.QueryToTable.Where(x => x.Dish.id == dishDb.id);
            foreach (var oldImage in oldImages)
            {
                Database.DishImage.Delete(oldImage);
            }

            foreach (var imgPath in dish.ImagePath)
            {
                var imgRow = new DishImage
                {
                    Dish = dishDb,
                    Path = imgPath,
                };
                dishDb.Images.Add(imgRow);
            }

            Database.Dish.Update(dishDb);
            Database.Save();
        }

        public void DeleteDish(int id)
        {
            var toDelete = Database.Dish.QueryToTable.FirstOrDefault(x => x.id == id);
            Database.Dish.Delete(toDelete);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
