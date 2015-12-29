namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReletionUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RolesForUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "Role_ID", c => c.Int());
            CreateIndex("dbo.Users", "Role_ID");
            AddForeignKey("dbo.Users", "Role_ID", "dbo.RolesForUsers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_ID", "dbo.RolesForUsers");
            DropIndex("dbo.Users", new[] { "Role_ID" });
            DropColumn("dbo.Users", "Role_ID");
            DropTable("dbo.RolesForUsers");
        }
    }
}
