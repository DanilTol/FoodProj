using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class DishImage :BaseEntity
    {
       
        public string Path { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
