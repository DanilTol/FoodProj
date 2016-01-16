using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FoodService.DAL.Entity
{
    public class UserSet : CommonClass
    {
        [Key]
        public int ID { get; set; }
        //public int OrderId { get; set; }
        //public int DishId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
