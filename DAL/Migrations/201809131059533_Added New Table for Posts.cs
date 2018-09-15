namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTableforPosts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubscriptionId = c.Int(nullable: false),
                        Title = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        Author = c.String(),
                        Body = c.String(),
                        Link = c.String(),
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
            DropForeignKey("dbo.Posts", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "UpdatedBy" });
            DropIndex("dbo.Posts", new[] { "CreatedBy" });
            DropTable("dbo.Posts");
        }
    }
}
