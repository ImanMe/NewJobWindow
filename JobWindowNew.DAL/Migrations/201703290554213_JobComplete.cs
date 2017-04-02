namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobComplete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "City", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Jobs", "Address", c => c.String());
            AddColumn("dbo.Jobs", "IsEverGreen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "SchedulingPod", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "OfficeId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "MinimumExperience", c => c.Short());
            AddColumn("dbo.Jobs", "MaximumExperience", c => c.Short());
            AddColumn("dbo.Jobs", "CompanyName", c => c.String(maxLength: 255));
            AddColumn("dbo.Jobs", "ActivationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "EmailTo", c => c.String(maxLength: 255));
            AddColumn("dbo.Jobs", "SId", c => c.Long());
            AddColumn("dbo.Jobs", "CreatedBy", c => c.String(nullable: false));
            AddColumn("dbo.Jobs", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "EditedBy", c => c.String(maxLength: 255));
            AddColumn("dbo.Jobs", "EditedDate", c => c.DateTime());
            AddColumn("dbo.Jobs", "Division", c => c.String(nullable: false));
            AddColumn("dbo.Jobs", "Currency", c => c.String(maxLength: 255));
            AddColumn("dbo.Jobs", "IsBestPerforming", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsOnlineApply", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "Author", c => c.String());
            AddColumn("dbo.Jobs", "JobBoardId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "EmploymentTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "SalaryTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "JobBoardId");
            CreateIndex("dbo.Jobs", "EmploymentTypeId");
            CreateIndex("dbo.Jobs", "SalaryTypeId");
            CreateIndex("dbo.Jobs", "CountryId");
            AddForeignKey("dbo.Jobs", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "EmploymentTypeId", "dbo.EmploymentTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "JobBoardId", "dbo.JobBoards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "SalaryTypeId", "dbo.SalaryTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "SalaryTypeId", "dbo.SalaryTypes");
            DropForeignKey("dbo.Jobs", "JobBoardId", "dbo.JobBoards");
            DropForeignKey("dbo.Jobs", "EmploymentTypeId", "dbo.EmploymentTypes");
            DropForeignKey("dbo.Jobs", "CountryId", "dbo.Countries");
            DropIndex("dbo.Jobs", new[] { "CountryId" });
            DropIndex("dbo.Jobs", new[] { "SalaryTypeId" });
            DropIndex("dbo.Jobs", new[] { "EmploymentTypeId" });
            DropIndex("dbo.Jobs", new[] { "JobBoardId" });
            DropColumn("dbo.Jobs", "CountryId");
            DropColumn("dbo.Jobs", "SalaryTypeId");
            DropColumn("dbo.Jobs", "EmploymentTypeId");
            DropColumn("dbo.Jobs", "JobBoardId");
            DropColumn("dbo.Jobs", "Author");
            DropColumn("dbo.Jobs", "IsOnlineApply");
            DropColumn("dbo.Jobs", "IsBestPerforming");
            DropColumn("dbo.Jobs", "Currency");
            DropColumn("dbo.Jobs", "Division");
            DropColumn("dbo.Jobs", "EditedDate");
            DropColumn("dbo.Jobs", "EditedBy");
            DropColumn("dbo.Jobs", "CreatedDate");
            DropColumn("dbo.Jobs", "CreatedBy");
            DropColumn("dbo.Jobs", "SId");
            DropColumn("dbo.Jobs", "EmailTo");
            DropColumn("dbo.Jobs", "ExpirationDate");
            DropColumn("dbo.Jobs", "ActivationDate");
            DropColumn("dbo.Jobs", "CompanyName");
            DropColumn("dbo.Jobs", "MaximumExperience");
            DropColumn("dbo.Jobs", "MinimumExperience");
            DropColumn("dbo.Jobs", "OfficeId");
            DropColumn("dbo.Jobs", "SchedulingPod");
            DropColumn("dbo.Jobs", "IsEverGreen");
            DropColumn("dbo.Jobs", "Address");
            DropColumn("dbo.Jobs", "City");
        }
    }
}
