using System;
using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class SetOnDay
    {
        public IEnumerable<DishModelShortInfo> DishModelShortInfos { get; set; }
        public DateTime Date { get; set; }
        public int Id { get; set; }
         
    }
}