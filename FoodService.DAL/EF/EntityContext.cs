using System.Data.Entity;
using FoodService.DAL.Entity;

namespace FoodService.DAL
{
    public class EntityContext: DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<OrderDish> OrderDish { get; set; }
        public DbSet<DayDishSet> DayDishSet { get; set; }
        public DbSet<DishImage> DishImage { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Report> Report { get; set; }
    }
}
