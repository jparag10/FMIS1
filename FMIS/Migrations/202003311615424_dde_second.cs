namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dde_second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries");
            DropIndex("dbo.Dieticians", new[] { "DieticianDataEntry_ddeID" });
            AddColumn("dbo.DieticianDataEntries", "dieticianid", c => c.Int());
            CreateIndex("dbo.DieticianDataEntries", "dieticianid");
            AddForeignKey("dbo.DieticianDataEntries", "dieticianid", "dbo.Dieticians", "did");
            DropColumn("dbo.Dieticians", "DieticianDataEntry_ddeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dieticians", "DieticianDataEntry_ddeID", c => c.Int());
            DropForeignKey("dbo.DieticianDataEntries", "dieticianid", "dbo.Dieticians");
            DropIndex("dbo.DieticianDataEntries", new[] { "dieticianid" });
            DropColumn("dbo.DieticianDataEntries", "dieticianid");
            CreateIndex("dbo.Dieticians", "DieticianDataEntry_ddeID");
            AddForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries", "ddeID");
        }
    }
}
