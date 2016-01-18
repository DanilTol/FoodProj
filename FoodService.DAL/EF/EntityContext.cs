using System.Data.Entity;
using FoodService.DAL.Entity;

namespace FoodService.DAL
{
    public class EntityContext: DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<UserSet> UserSet { get; set; }
        public DbSet<DayDishSet> DayDishSet { get; set; }
        public DbSet<DishToImage> DishToImage { get; set; }
        public DbSet<RolesForUser> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}
