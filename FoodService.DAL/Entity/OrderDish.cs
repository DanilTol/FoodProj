namespace FoodService.DAL.Entity
{
    public class OrderDish : BaseEntity
    {
        public int Count { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Order Order { get; set; }
    }
}
