namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weekToDay : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WeekDishSets", newName: "DayDishSets");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DayDishSets", newName: "WeekDishSets");
        }
    }
}
