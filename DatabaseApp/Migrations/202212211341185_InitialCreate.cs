namespace DatabaseApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Distributions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        WorkWearId = c.Int(nullable: false),
                        Issued = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.WorkWears", t => t.WorkWearId)
                .Index(t => t.InvoiceId)
                .Index(t => t.WorkWearId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkerId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workers", t => t.WorkerId)
                .Index(t => t.WorkerId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        WorkshopId = c.Int(nullable: false),
                        FIO = c.String(),
                        Discount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.JobId)
                .ForeignKey("dbo.Workshops", t => t.WorkshopId)
                .Index(t => t.JobId)
                .Index(t => t.WorkshopId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workshops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkWears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WearTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Cost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WearTypes", t => t.WearTypeId)
                .Index(t => t.WearTypeId);
            
            CreateTable(
                "dbo.WearTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkWears", "WearTypeId", "dbo.WearTypes");
            DropForeignKey("dbo.Distributions", "WorkWearId", "dbo.WorkWears");
            DropForeignKey("dbo.Workers", "WorkshopId", "dbo.Workshops");
            DropForeignKey("dbo.Workers", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Invoices", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.Distributions", "InvoiceId", "dbo.Invoices");
            DropIndex("dbo.WorkWears", new[] { "WearTypeId" });
            DropIndex("dbo.Workers", new[] { "WorkshopId" });
            DropIndex("dbo.Workers", new[] { "JobId" });
            DropIndex("dbo.Invoices", new[] { "WorkerId" });
            DropIndex("dbo.Distributions", new[] { "WorkWearId" });
            DropIndex("dbo.Distributions", new[] { "InvoiceId" });
            DropTable("dbo.WearTypes");
            DropTable("dbo.WorkWears");
            DropTable("dbo.Workshops");
            DropTable("dbo.Jobs");
            DropTable("dbo.Workers");
            DropTable("dbo.Invoices");
            DropTable("dbo.Distributions");
        }
    }
}
