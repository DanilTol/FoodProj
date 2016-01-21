namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableOrderDishesWithFieldCount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDishes", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.OrderDishes", "Dish_id", "dbo.Dishes");
            DropIndex("dbo.OrderDishes", new[] { "Order_id" });
            DropIndex("dbo.OrderDishes", new[] { "Dish_id" });
            DropPrimaryKey("dbo.OrderDishes");
            AddColumn("dbo.Orders", "Checked", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDishes", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.OrderDishes", "Count", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDishes", "Order_id", c => c.Int());
            AlterColumn("dbo.OrderDishes", "Dish_id", c => c.Int());
            AddPrimaryKey("dbo.OrderDishes", "id");
            CreateIndex("dbo.OrderDishes", "Dish_id");
            CreateIndex("dbo.OrderDishes", "Order_id");
            AddForeignKey("dbo.OrderDishes", "Order_id", "dbo.Orders", "id");
            AddForeignKey("dbo.OrderDishes", "Dish_id", "dbo.Dishes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDishes", "Dish_id", "dbo.Dishes");
            DropForeignKey("dbo.OrderDishes", "Order_id", "dbo.Orders");
            DropIndex("dbo.OrderDishes", new[] { "Order_id" });
            DropIndex("dbo.OrderDishes", new[] { "Dish_id" });
            DropPrimaryKey("dbo.OrderDishes");
            AlterColumn("dbo.OrderDishes", "Dish_id", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDishes", "Order_id", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDishes", "Count");
            DropColumn("dbo.OrderDishes", "id");
            DropColumn("dbo.Orders", "Checked");
            AddPrimaryKey("dbo.OrderDishes", new[] { "Order_id", "Dish_id" });
            CreateIndex("dbo.OrderDishes", "Dish_id");
            CreateIndex("dbo.OrderDishes", "Order_id");
            AddForeignKey("dbo.OrderDishes", "Dish_id", "dbo.Dishes", "id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDishes", "Order_id", "dbo.Orders", "id", cascadeDelete: true);
        }
    }
}
