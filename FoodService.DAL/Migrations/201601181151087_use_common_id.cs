namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class use_common_id : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DayDishSets", new[] { "Dish_ID" });
            DropIndex("dbo.DishToImages", new[] { "Dish_ID" });
            DropIndex("dbo.UserSets", new[] { "Dish_ID" });
            DropIndex("dbo.UserSets", new[] { "Order_ID" });
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropIndex("dbo.Users", new[] { "Role_ID" });
            CreateIndex("dbo.DayDishSets", "Dish_id");
            CreateIndex("dbo.DishToImages", "Dish_id");
            CreateIndex("dbo.UserSets", "Dish_id");
            CreateIndex("dbo.UserSets", "Order_id");
            CreateIndex("dbo.Orders", "User_id");
            CreateIndex("dbo.Users", "Role_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Role_id" });
            DropIndex("dbo.Orders", new[] { "User_id" });
            DropIndex("dbo.UserSets", new[] { "Order_id" });
            DropIndex("dbo.UserSets", new[] { "Dish_id" });
            DropIndex("dbo.DishToImages", new[] { "Dish_id" });
            DropIndex("dbo.DayDishSets", new[] { "Dish_id" });
            CreateIndex("dbo.Users", "Role_ID");
            CreateIndex("dbo.Orders", "User_ID");
            CreateIndex("dbo.UserSets", "Order_ID");
            CreateIndex("dbo.UserSets", "Dish_ID");
            CreateIndex("dbo.DishToImages", "Dish_ID");
            CreateIndex("dbo.DayDishSets", "Dish_ID");
        }
    }
}
