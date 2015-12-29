using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodService.DAL.Entity
{
    public class WeekDishSet : CommonClass
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public int DishId { get; set; } // need to delete

        //public virtual Order Order { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
