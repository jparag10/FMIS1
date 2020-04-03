namespace FMIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FMS : DbMigration
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
            
            CreateTable(
                "dbo.Dieticians",
                c => new
                    {
                        did = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        Password = c.String(nullable: false),
                        Contact = c.Long(nullable: false),
                        Location = c.String(nullable: false),
                        Experience = c.Int(nullable: false),
                        filepath = c.String(),
                    })
                .PrimaryKey(t => t.did)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userid = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        ContactNo = c.Long(nullable: false),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DieticianDataEntries", "Dietician_did", "dbo.Dieticians");
            DropIndex("dbo.Dieticians", new[] { "Email" });
            DropIndex("dbo.DieticianDataEntries", new[] { "Dietician_did" });
            DropTable("dbo.Users");
            DropTable("dbo.Dieticians");
            DropTable("dbo.DieticianDataEntries");
        }
    }
}
