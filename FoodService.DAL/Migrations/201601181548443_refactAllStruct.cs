namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactAllStruct : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RolesForUsers", newName: "Roles");
            DropForeignKey("dbo.DishToImages", "Dish_id", "dbo.Dishes");
            DropForeignKey("dbo.UserSets", "Dish_id", "dbo.Dishes");
            DropForeignKey("dbo.UserSets", "Order_id", "dbo.Orders");
            DropIndex("dbo.DishToImages", new[] { "Dish_id" });
            DropIndex("dbo.UserSets", new[] { "Dish_id" });
            DropIndex("dbo.UserSets", new[] { "Order_id" });
            CreateTable(
                "dbo.DishImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Dish_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Dishes", t => t.Dish_id)
                .Index(t => t.Dish_id);
            
            AddColumn("dbo.Dishes", "Order_id", c => c.Int());
            CreateIndex("dbo.Dishes", "Order_id");
            AddForeignKey("dbo.Dishes", "Order_id", "dbo.Orders", "id");
            DropTable("dbo.DishToImages");
            DropTable("dbo.UserSets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserSets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Dish_id = c.Int(),
                        Order_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.DishToImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PathToImageOnServer = c.String(),
                        Dish_id = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.Dishes", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.DishImages", "Dish_id", "dbo.Dishes");
            DropIndex("dbo.DishImages", new[] { "Dish_id" });
            DropIndex("dbo.Dishes", new[] { "Order_id" });
            DropColumn("dbo.Dishes", "Order_id");
            DropTable("dbo.DishImages");
            CreateIndex("dbo.UserSets", "Order_id");
            CreateIndex("dbo.UserSets", "Dish_id");
            CreateIndex("dbo.DishToImages", "Dish_id");
            AddForeignKey("dbo.UserSets", "Order_id", "dbo.Orders", "id");
            AddForeignKey("dbo.UserSets", "Dish_id", "dbo.Dishes", "id");
            AddForeignKey("dbo.DishToImages", "Dish_id", "dbo.Dishes", "id");
            RenameTable(name: "dbo.Roles", newName: "RolesForUsers");
        }
    }
}
