namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addManyToMany_DishToOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dishes", "Order_id", "dbo.Orders");
            DropIndex("dbo.Dishes", new[] { "Order_id" });
            CreateTable(
                "dbo.OrderDishes",
                c => new
                    {
                        Order_id = c.Int(nullable: false),
                        Dish_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_id, t.Dish_id })
                .ForeignKey("dbo.Orders", t => t.Order_id, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_id, cascadeDelete: true)
                .Index(t => t.Order_id)
                .Index(t => t.Dish_id);
            
            DropColumn("dbo.Dishes", "Order_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dishes", "Order_id", c => c.Int());
            DropForeignKey("dbo.OrderDishes", "Dish_id", "dbo.Dishes");
            DropForeignKey("dbo.OrderDishes", "Order_id", "dbo.Orders");
            DropIndex("dbo.OrderDishes", new[] { "Dish_id" });
            DropIndex("dbo.OrderDishes", new[] { "Order_id" });
            DropTable("dbo.OrderDishes");
            CreateIndex("dbo.Dishes", "Order_id");
            AddForeignKey("dbo.Dishes", "Order_id", "dbo.Orders", "id");
        }
    }
}
