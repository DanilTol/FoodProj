namespace FoodService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(maxLength: 50),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        ChefReport = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
