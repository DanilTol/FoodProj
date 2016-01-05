namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clearDaySet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DayDishSets", "DishId", "dbo.Dishes");
            DropIndex("dbo.DayDishSets", new[] { "DishId" });
            RenameColumn(table: "dbo.DayDishSets", name: "DishId", newName: "Dish_ID");
            AlterColumn("dbo.DayDishSets", "Dish_ID", c => c.Int());
            CreateIndex("dbo.DayDishSets", "Dish_ID");
            AddForeignKey("dbo.DayDishSets", "Dish_ID", "dbo.Dishes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayDishSets", "Dish_ID", "dbo.Dishes");
            DropIndex("dbo.DayDishSets", new[] { "Dish_ID" });
            AlterColumn("dbo.DayDishSets", "Dish_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.DayDishSets", name: "Dish_ID", newName: "DishId");
            CreateIndex("dbo.DayDishSets", "DishId");
            AddForeignKey("dbo.DayDishSets", "DishId", "dbo.Dishes", "ID", cascadeDelete: true);
        }
    }
}
