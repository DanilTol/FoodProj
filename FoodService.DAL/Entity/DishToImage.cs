using System.ComponentModel.DataAnnotations;

namespace FoodService.DAL.Entity
{
    public class DishToImage :CommonClass
    {
       
        public string PathToImageOnServer { get; set; }

        public virtual Dish Dish { get; set; }
    }
}
