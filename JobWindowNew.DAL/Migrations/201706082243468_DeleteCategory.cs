namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "Category", c => c.String());
        }
    }
}
