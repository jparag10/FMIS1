namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userpage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries");
            DropIndex("dbo.Dieticians", new[] { "DieticianDataEntry_ddeID" });
            AlterColumn("dbo.DieticianDataEntries", "Disease", c => c.String());
            DropColumn("dbo.Dieticians", "DieticianDataEntry_ddeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dieticians", "DieticianDataEntry_ddeID", c => c.Int());
            AlterColumn("dbo.DieticianDataEntries", "Disease", c => c.Int(nullable: false));
            DropColumn("dbo.DieticianDataEntries", "dieticianid");
            CreateIndex("dbo.Dieticians", "DieticianDataEntry_ddeID");
            AddForeignKey("dbo.Dieticians", "DieticianDataEntry_ddeID", "dbo.DieticianDataEntries", "ddeID");
        }
    }
}
