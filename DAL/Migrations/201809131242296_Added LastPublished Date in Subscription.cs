namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLastPublishedDateinSubscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "LastPublishedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "LastPublishedDate");
        }
    }
}
