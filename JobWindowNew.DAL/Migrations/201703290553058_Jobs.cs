namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jobs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.JobLists", newName: "Jobs");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Jobs", newName: "JobLists");
        }
    }
}
