namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueUserEmailSetLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "EmailAddress", c => c.String(maxLength: 50));
            CreateIndex("dbo.Users", "EmailAddress", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "EmailAddress" });
            AlterColumn("dbo.Users", "EmailAddress", c => c.String());
        }
    }
}
