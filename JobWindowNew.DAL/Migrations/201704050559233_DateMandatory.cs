namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateMandatory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "ActivationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "ExpirationDate", c => c.DateTime());
            AlterColumn("dbo.Jobs", "ActivationDate", c => c.DateTime());
        }
    }
}
