using System;
using System.Collections.Generic;
using FoodService.Business.DTO;

namespace FoodService.Business.ServiceInterfaces
{
    public interface IDaySetService
    {
        void UpdateDayDishSet(DateTime date, int[] dishIds);
        IEnumerable<DishModelShortInfo> Filter(DateTime dateTime, string filter);
        void Dispose();

    }
}