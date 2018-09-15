namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscriptionEntityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        URL = c.String(),
                        CreatedDateTime = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedDateTime = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Subscriptions", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "UpdatedBy" });
            DropIndex("dbo.Subscriptions", new[] { "CreatedBy" });
            DropTable("dbo.Subscriptions");
        }
    }
}
