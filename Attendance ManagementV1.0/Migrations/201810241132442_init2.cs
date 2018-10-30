namespace Attendance_ManagementV1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.FacultyCourses");
            AddColumn("dbo.FacultyCourses", "DivisionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.FacultyCourses", new[] { "FacultyId", "CourseId", "DivisionId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FacultyCourses");
            DropColumn("dbo.FacultyCourses", "DivisionId");
            AddPrimaryKey("dbo.FacultyCourses", new[] { "FacultyId", "CourseId" });
        }
    }
}
