using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class DayDishSet : CommonClass
    {
       
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
