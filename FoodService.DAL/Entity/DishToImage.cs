using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class DishToImage :CommonClass
    {
        [Key]
        public int ID { get; set; }
        public string PathToImageOnServer { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
