namespace MemberLoginSSRS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class layout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Layout", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Layout");
        }
    }
}
