namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        JobId = c.Long(nullable: false),
                        Email = c.String(nullable: false, maxLength: 255),
                        Phone = c.Long(nullable: false),
                        FileName = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applicants");
        }
    }
}
