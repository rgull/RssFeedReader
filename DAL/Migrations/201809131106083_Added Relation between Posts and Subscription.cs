namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRelationbetweenPostsandSubscription : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Posts", "SubscriptionId");
            AddForeignKey("dbo.Posts", "SubscriptionId", "dbo.Subscriptions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "SubscriptionId", "dbo.Subscriptions");
            DropIndex("dbo.Posts", new[] { "SubscriptionId" });
        }
    }
}
