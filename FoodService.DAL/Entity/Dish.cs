using System.Collections.Generic;

namespace FoodService.DAL.Entity
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; }
        public string Ingridients { get; set; }
        public int Energy { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderDish> OrdersDishes { get; set; }
        public virtual ICollection<DishImage> Images { get; set; }
    }
}