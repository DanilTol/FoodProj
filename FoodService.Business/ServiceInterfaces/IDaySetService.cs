using System;
using System.Collections.Generic;
using FoodService.Business.DTO;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IDaySetService
    {
        IEnumerable<DishModelShortInfo> GetWeekInfo(DateTime dateTime);
        void DeleteAndEditWeekDishSet(DateTime date, int[] dishIds);
        void Dispose();

    }
}