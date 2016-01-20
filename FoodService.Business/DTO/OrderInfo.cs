using System;
using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<DishModelShortInfo> Dishes{get;set;}
        public DateTime Date { get; set; }
    }
}