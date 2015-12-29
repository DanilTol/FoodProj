using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class BigDishAndWeekDishSet
    {
        public IEnumerable<DishModelShortInfo> CertainDishSets { get; set; }
        public IEnumerable<DishModelShortInfo> AllDishes { get; set; }
    }
}