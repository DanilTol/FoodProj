using System;
using System.Collections.Generic;

namespace FoodService.Business.DTO
{
    public class OrderPlates
    {
        //public OrderPlates() { }

        //public OrderPlates( DateTime date, int userid, IEnumerable<DishModelShortInfo> infos)
        //{
            
        //    this.date = date;
        //    this.userId = userid;
        //    Infos = infos;
        //}

        
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public IEnumerable<DishModelShortInfo> Infos { get; set; } 

    }
}