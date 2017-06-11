namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobBoardChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobBoards", "IsOnlineApply", c => c.Boolean(nullable: false));
            AddColumn("dbo.JobBoards", "IsEmailApply", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobBoards", "IsEmailApply");
            DropColumn("dbo.JobBoards", "IsOnlineApply");
        }
    }
}
