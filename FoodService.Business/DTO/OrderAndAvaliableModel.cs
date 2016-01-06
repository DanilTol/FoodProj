using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class OrderAndAvaliableModel
    {
       public IEnumerable<DishModelShortInfo> OrderPlateses { get; set; }
       public IEnumerable<DishModelShortInfo> SetOnDays { get; set; }
    }
}