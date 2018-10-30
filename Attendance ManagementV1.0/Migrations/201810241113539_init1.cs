namespace Attendance_ManagementV1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentAttendances", "DivisonId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentAttendances", "DivisonId");
        }
    }
}
