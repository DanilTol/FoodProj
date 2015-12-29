using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class OrderAndAvaliableModel
    {
        //public OrderAndAvaliableModel() { }

        //public OrderAndAvaliableModel(IEnumerable<DishModelShortInfo> orderPlateses, IEnumerable<DishModelShortInfo> setOnDays)
        //{
        //    OrderPlateses = orderPlateses;
        //    SetOnDays = setOnDays;

        //}

       public IEnumerable<DishModelShortInfo> OrderPlateses { get; set; }
       public IEnumerable<DishModelShortInfo> SetOnDays { get; set; }
    }
}