namespace JobWindowNew.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MappingTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobCategoryMaps",
                c => new
                {
                    JobId = c.Long(nullable: false),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.JobId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.JobOccupationMaps",
                c => new
                {
                    JobId = c.Long(nullable: false),
                    OccupationId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.JobId, t.OccupationId })
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Occupations", t => t.OccupationId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.OccupationId);


        }

        public override void Down()
        {
            DropForeignKey("dbo.JobOccupationMaps", "OccupationId", "dbo.Occupations");
            DropForeignKey("dbo.JobOccupationMaps", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobCategoryMaps", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.JobCategoryMaps", "CategoryId", "dbo.Categories");
            DropIndex("dbo.JobOccupationMaps", new[] { "OccupationId" });
            DropIndex("dbo.JobOccupationMaps", new[] { "JobId" });
            DropIndex("dbo.JobCategoryMaps", new[] { "CategoryId" });
            DropIndex("dbo.JobCategoryMaps", new[] { "JobId" });

            DropTable("dbo.JobOccupationMaps");
            DropTable("dbo.JobCategoryMaps");
        }
    }
}
