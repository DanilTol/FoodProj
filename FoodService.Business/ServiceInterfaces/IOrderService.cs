using System;
using System.Collections.Generic;
using FoodService.Business.DTO;
using FoodService.DAL.Entity;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IOrderService
    {
        IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, User user);
        void DeleteOldAndAddNewOrder(DateTime date, int[] arraInts, User user);
        void Dispose();
    }
}