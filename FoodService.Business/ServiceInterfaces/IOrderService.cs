using System;
using System.Collections.Generic;
using FoodService.Business.DTO;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IOrderService
    {
        IEnumerable<DishModelShortInfo> GetPlatesByDate(DateTime date, string email);
        void EditOrder(DateTime date, int[] arraInts, string email);
        void DeleteOrder(DateTime date, string email);
        void Dispose();
    }
}