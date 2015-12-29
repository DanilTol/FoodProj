namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFieldLocked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsLocked");
        }
    }
}
