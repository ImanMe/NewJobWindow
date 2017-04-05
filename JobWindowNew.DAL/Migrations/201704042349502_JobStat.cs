namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobStat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CloneFrom", c => c.Long());
            AddColumn("dbo.Jobs", "EverGreenId", c => c.Long());
            AddColumn("dbo.Jobs", "Bob", c => c.Int());
            AddColumn("dbo.Jobs", "Intvs", c => c.Int());
            AddColumn("dbo.Jobs", "Intvs2", c => c.Int());
            AddColumn("dbo.Jobs", "ApsCl", c => c.Int());
            AddColumn("dbo.Jobs", "RemovedCl", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Jobs", "RemovedReason", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "RemovedReason");
            DropColumn("dbo.Jobs", "RemovedCl");
            DropColumn("dbo.Jobs", "ApsCl");
            DropColumn("dbo.Jobs", "Intvs2");
            DropColumn("dbo.Jobs", "Intvs");
            DropColumn("dbo.Jobs", "Bob");
            DropColumn("dbo.Jobs", "EverGreenId");
            DropColumn("dbo.Jobs", "CloneFrom");
        }
    }
}
