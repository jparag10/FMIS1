namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DieticianDataEntries",
                c => new
                    {
                        ddeID = c.Int(nullable: false, identity: true),
                        Disease = c.Int(nullable: false),
                        WhatToEat = c.String(),
                        NotToEat = c.String(),
                        Dietician_did = c.Int(),
                    })
                .PrimaryKey(t => t.ddeID)
                .ForeignKey("dbo.Dieticians", t => t.Dietician_did)
                .Index(t => t.Dietician_did);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DieticianDataEntries", "Dietician_did", "dbo.Dieticians");
            DropIndex("dbo.DieticianDataEntries", new[] { "Dietician_did" });
            DropTable("dbo.DieticianDataEntries");
        }
    }
}
