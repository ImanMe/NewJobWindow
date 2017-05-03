namespace JobWindowNew.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CategoryActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "Category", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Jobs", "Category");
            DropColumn("dbo.Jobs", "IsActive");

        }
    }
}
