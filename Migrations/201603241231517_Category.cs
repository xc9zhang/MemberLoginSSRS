namespace MemberLoginSSRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CategoryAssignment",
                c => new
                    {
                        CategoryAssignmentID = c.Int(nullable: false, identity: true),
                        TEReportID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryAssignmentID)
                .ForeignKey("dbo.Category", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.TEReport", t => t.TEReportID, cascadeDelete: true)
                .Index(t => t.TEReportID)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryAssignment", "TEReportID", "dbo.TEReport");
            DropForeignKey("dbo.CategoryAssignment", "CategoryID", "dbo.Category");
            DropIndex("dbo.CategoryAssignment", new[] { "CategoryID" });
            DropIndex("dbo.CategoryAssignment", new[] { "TEReportID" });
            DropTable("dbo.CategoryAssignment");
            DropTable("dbo.Category");
        }
    }
}
