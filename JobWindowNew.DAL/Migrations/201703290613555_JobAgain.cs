namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "StateId");
            AddForeignKey("dbo.Jobs", "StateId", "dbo.States", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "StateId", "dbo.States");
            DropIndex("dbo.Jobs", new[] { "StateId" });
            DropColumn("dbo.Jobs", "StateId");
        }
    }
}
