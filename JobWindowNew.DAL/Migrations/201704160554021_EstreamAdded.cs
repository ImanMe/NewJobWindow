namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstreamAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobBoards", "EstreamJBsID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobBoards", "EstreamJBsID");
        }
    }
}
