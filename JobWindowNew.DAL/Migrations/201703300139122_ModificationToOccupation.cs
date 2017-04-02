namespace JobWindowNew.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificationToOccupation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Occupations", "SId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Occupations", "SId", c => c.Long(nullable: false));
        }
    }
}
