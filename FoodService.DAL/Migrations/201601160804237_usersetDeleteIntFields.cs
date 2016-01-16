namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usersetDeleteIntFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSets", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.UserSets", "OrderId", "dbo.Orders");
            DropIndex("dbo.UserSets", new[] { "OrderId" });
            DropIndex("dbo.UserSets", new[] { "DishId" });
            RenameColumn(table: "dbo.UserSets", name: "DishId", newName: "Dish_ID");
            RenameColumn(table: "dbo.UserSets", name: "OrderId", newName: "Order_ID");
            AlterColumn("dbo.UserSets", "Order_ID", c => c.Int());
            AlterColumn("dbo.UserSets", "Dish_ID", c => c.Int());
            CreateIndex("dbo.UserSets", "Dish_ID");
            CreateIndex("dbo.UserSets", "Order_ID");
            AddForeignKey("dbo.UserSets", "Dish_ID", "dbo.Dishes", "ID");
            AddForeignKey("dbo.UserSets", "Order_ID", "dbo.Orders", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSets", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.UserSets", "Dish_ID", "dbo.Dishes");
            DropIndex("dbo.UserSets", new[] { "Order_ID" });
            DropIndex("dbo.UserSets", new[] { "Dish_ID" });
            AlterColumn("dbo.UserSets", "Dish_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.UserSets", "Order_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserSets", name: "Order_ID", newName: "OrderId");
            RenameColumn(table: "dbo.UserSets", name: "Dish_ID", newName: "DishId");
            CreateIndex("dbo.UserSets", "DishId");
            CreateIndex("dbo.UserSets", "OrderId");
            AddForeignKey("dbo.UserSets", "OrderId", "dbo.Orders", "ID", cascadeDelete: true);
            AddForeignKey("dbo.UserSets", "DishId", "dbo.Dishes", "ID", cascadeDelete: true);
        }
    }
}
