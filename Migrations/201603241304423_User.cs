namespace MemberLoginSSRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleAssignment",
                c => new
                    {
                        RoleAssignmentID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleAssignmentID)
                .ForeignKey("dbo.Role", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleAssignment", "UserID", "dbo.User");
            DropForeignKey("dbo.RoleAssignment", "RoleID", "dbo.Role");
            DropIndex("dbo.RoleAssignment", new[] { "RoleID" });
            DropIndex("dbo.RoleAssignment", new[] { "UserID" });
            DropTable("dbo.User");
            DropTable("dbo.RoleAssignment");
        }
    }
}
