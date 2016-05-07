namespace MemberLoginSSRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportAssignment",
                c => new
                    {
                        ReportAssignmentID = c.Int(nullable: false, identity: true),
                        TEReportID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportAssignmentID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.TEReport", t => t.TEReportID, cascadeDelete: true)
                .Index(t => t.TEReportID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TEReport",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SsrsServerUrl = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportAssignment", "TEReportID", "dbo.TEReport");
            DropForeignKey("dbo.ReportAssignment", "RoleID", "dbo.Role");
            DropIndex("dbo.ReportAssignment", new[] { "RoleID" });
            DropIndex("dbo.ReportAssignment", new[] { "TEReportID" });
            DropTable("dbo.TEReport");
            DropTable("dbo.Role");
            DropTable("dbo.ReportAssignment");
        }
    }
}
