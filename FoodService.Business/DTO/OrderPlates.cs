using System;
using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class OrderPlates
    {
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public IEnumerable<DishModelShortInfo> Infos { get; set; } 

    }
}