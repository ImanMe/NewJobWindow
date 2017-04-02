namespace JobWindowNew.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class MyInitials : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.JobLists",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 255),
                    JobDescription = c.String(nullable: false),
                    JobRequirements = c.String(nullable: false),
                    Salary = c.Decimal(precision: 18, scale: 2),
                    ZipCode = c.String(nullable: false, maxLength: 255),
                })
                .PrimaryKey(t => t.Id);


        }

        public override void Down()
        {
            CreateTable(
                "dbo.JobPostings",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 255),
                    JobDescription = c.String(nullable: false),
                    JobRequirements = c.String(nullable: false),
                    Salary = c.Decimal(precision: 18, scale: 2),
                    ZipCode = c.String(nullable: false, maxLength: 255),
                    City = c.String(nullable: false, maxLength: 255),
                    Address = c.String(),
                    IsEverGreen = c.Boolean(nullable: false),
                    SchedulingPod = c.Int(nullable: false),
                    OfficeId = c.Int(nullable: false),
                    MinimumExperience = c.Short(),
                    MaximumExperience = c.Short(),
                    CompanyName = c.String(maxLength: 255),
                    ActivationDate = c.DateTime(nullable: false),
                    ExpirationDate = c.DateTime(nullable: false),
                    EmailTo = c.String(maxLength: 255),
                    SId = c.Long(),
                    CreatedBy = c.String(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    EditedBy = c.String(maxLength: 255),
                    EditedDate = c.DateTime(),
                    Division = c.String(nullable: false),
                    Currency = c.String(maxLength: 255),
                    IsBestPerforming = c.Boolean(nullable: false),
                    IsOnlineApply = c.Boolean(nullable: false),
                    Author = c.String(),
                    JobBoardId = c.Int(nullable: false),
                    EmploymentTypeId = c.Int(nullable: false),
                    SalaryTypeId = c.Int(nullable: false),
                    CountryId = c.Int(nullable: false),
                    StateId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            DropTable("dbo.JobLists");
            CreateIndex("dbo.JobPostings", "StateId");
            CreateIndex("dbo.JobPostings", "CountryId");
            CreateIndex("dbo.JobPostings", "SalaryTypeId");
            CreateIndex("dbo.JobPostings", "EmploymentTypeId");
            CreateIndex("dbo.JobPostings", "JobBoardId");
            AddForeignKey("dbo.JobPostings", "StateId", "dbo.States", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobPostings", "SalaryTypeId", "dbo.SalaryTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobPostings", "JobBoardId", "dbo.JobBoards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobPostings", "EmploymentTypeId", "dbo.EmploymentTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JobPostings", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
        }
    }
}
