using System;
using System.Data.Entity;
using FoodService.DAL.Entity;

namespace FoodService.DAL
{
    public class EntityContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<UserSet> UserSets { get; set; }
        public DbSet<DayDishSet> DayDishSets { get; set; }
        public DbSet<DishToImage> DishToImage { get; set; }
        public DbSet<RolesForUser> RolesForUsers { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
