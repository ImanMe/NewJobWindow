namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicantPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "Phone", c => c.String(maxLength: 13));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "Phone", c => c.Long(nullable: false));
        }
    }
}
