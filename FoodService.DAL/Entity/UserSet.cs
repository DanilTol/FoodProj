using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FoodService.DAL.Entity
{
    public class UserSet : CommonClass
    {
        public virtual Order Order { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
