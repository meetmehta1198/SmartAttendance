namespace Attendance_ManagementV1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "username", c => c.String());
            AddColumn("dbo.Faculties", "username", c => c.String());
            AddColumn("dbo.Students", "username", c => c.String());
            DropTable("dbo.logins");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.logins",
                c => new
                    {
                        username = c.Int(nullable: false, identity: true),
                        password = c.String(),
                        usertype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
            DropColumn("dbo.Students", "username");
            DropColumn("dbo.Faculties", "username");
            DropColumn("dbo.Admins", "username");
        }
    }
}
