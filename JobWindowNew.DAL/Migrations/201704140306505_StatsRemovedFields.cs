namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatsRemovedFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "RemovedCl");
            DropColumn("dbo.Jobs", "RemovedReason");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "RemovedReason", c => c.String(maxLength: 100));
            AddColumn("dbo.Jobs", "RemovedCl", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
