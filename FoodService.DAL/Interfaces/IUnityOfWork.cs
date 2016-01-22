using System;
using FoodService.DAL.RepositoryBag;

namespace FoodService.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DishRepository Dish { get; }
        OrderRepository Order { get; }
        DishImageRepository DishImage { get; }
        DayDishSetRepository DayDish { get; }
        UserRepository User { get; }
        RoleForUserRepository Role { get; }
        OrderDishRepository OrderDishes { get; }
        ReportRepository Report { get; }
        void Save();

    }
}