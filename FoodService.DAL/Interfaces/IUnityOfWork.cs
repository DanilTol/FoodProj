﻿using System;
using FoodService.DAL.Entity;
using FoodService.DAL.RepositoryBag;

namespace FoodService.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DishRepository Dish { get; }
        OrderRepository Order { get; }
        DishToImageRepository DishToImage { get; }
        DayDishSetRepository DayDish { get; }
        UserRepository User { get; }
        RoleForUserRepository Role { get; }
        void Save();

    }
}