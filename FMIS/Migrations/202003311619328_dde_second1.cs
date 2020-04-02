namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dde_second1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DieticianDataEntries", "dieticianid", "dbo.Dieticians");
            DropIndex("dbo.DieticianDataEntries", new[] { "dieticianid" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.DieticianDataEntries", "dieticianid");
            AddForeignKey("dbo.DieticianDataEntries", "dieticianid", "dbo.Dieticians", "did");
        }
    }
}
