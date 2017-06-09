namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CategoryId", c => c.Int());
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropColumn("dbo.Jobs", "CategoryId");
        }
    }
}
