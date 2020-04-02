namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dde_first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DieticianDataEntries", "Dietician_did", "dbo.Dieticians");
            DropIndex("dbo.DieticianDataEntries", new[] { "Dietician_did" });
            AddColumn("dbo.Dieticians", "DieticianDataEntry_ddeID", c => c.Int());
            CreateIndex("dbo.Dieticians", "DieticianDataEntry_ddeID");
            AddForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries", "ddeID");
            DropColumn("dbo.DieticianDataEntries", "Dietician_did");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DieticianDataEntries", "Dietician_did", c => c.Int());
            DropForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries");
            DropIndex("dbo.Dieticians", new[] { "DieticianDataEntry_ddeID" });
            DropColumn("dbo.Dieticians", "DieticianDataEntry_ddeID");
            CreateIndex("dbo.DieticianDataEntries", "Dietician_did");
            AddForeignKey("dbo.DieticianDataEntries", "Dietician_did", "dbo.Dieticians", "did");
        }
    }
}
