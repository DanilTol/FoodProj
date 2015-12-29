namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReletionUserOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "User_ID", c => c.Int());
            CreateIndex("dbo.Orders", "User_ID");
            AddForeignKey("dbo.Orders", "User_ID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_ID", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropColumn("dbo.Orders", "User_ID");
        }
    }
}
