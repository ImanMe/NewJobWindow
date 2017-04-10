namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasStatsField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "HasStats", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "HasStats");
        }
    }
}
