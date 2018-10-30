namespace Attendance_ManagementV1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        AdminName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        Sem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.DivisionCourses",
                c => new
                    {
                        DivisionId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DivisionId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionId = c.Int(nullable: false, identity: true),
                        DivisionName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DivisionId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyId = c.Int(nullable: false, identity: true),
                        FacultyName = c.String(),
                        password = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        Mobilenumber = c.String(),
                        Division_DivisionId = c.Int(),
                    })
                .PrimaryKey(t => t.FacultyId)
                .ForeignKey("dbo.Divisions", t => t.Division_DivisionId)
                .Index(t => t.Division_DivisionId);
            
            CreateTable(
                "dbo.FacultyCourses",
                c => new
                    {
                        FacultyId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.FacultyDivisions",
                c => new
                    {
                        FacultyId = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacultyId, t.DivisionId })
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .Index(t => t.FacultyId)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.StudentFaculties",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        FacultyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.FacultyId })
                .ForeignKey("dbo.Faculties", t => t.FacultyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        password = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Branch = c.String(),
                        Sem = c.Int(nullable: false),
                        DivisionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Divisions", t => t.DivisionId, cascadeDelete: true)
                .Index(t => t.DivisionId);
            
            CreateTable(
                "dbo.StudentAttendances",
                c => new
                    {
                        StudentAttendenceId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentAttendenceId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.logins",
                c => new
                    {
                        username = c.Int(nullable: false, identity: true),
                        password = c.String(),
                        usertype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Faculties", "Division_DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.StudentFaculties", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentAttendances", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentAttendances", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Students", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.StudentFaculties", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.FacultyDivisions", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.FacultyDivisions", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.FacultyCourses", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.FacultyCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.DivisionCourses", "DivisionId", "dbo.Divisions");
            DropForeignKey("dbo.DivisionCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
            DropIndex("dbo.StudentAttendances", new[] { "StudentId" });
            DropIndex("dbo.StudentAttendances", new[] { "CourseId" });
            DropIndex("dbo.Students", new[] { "DivisionId" });
            DropIndex("dbo.StudentFaculties", new[] { "FacultyId" });
            DropIndex("dbo.StudentFaculties", new[] { "StudentId" });
            DropIndex("dbo.FacultyDivisions", new[] { "DivisionId" });
            DropIndex("dbo.FacultyDivisions", new[] { "FacultyId" });
            DropIndex("dbo.FacultyCourses", new[] { "CourseId" });
            DropIndex("dbo.FacultyCourses", new[] { "FacultyId" });
            DropIndex("dbo.Faculties", new[] { "Division_DivisionId" });
            DropIndex("dbo.DivisionCourses", new[] { "CourseId" });
            DropIndex("dbo.DivisionCourses", new[] { "DivisionId" });
            DropTable("dbo.logins");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.StudentAttendances");
            DropTable("dbo.Students");
            DropTable("dbo.StudentFaculties");
            DropTable("dbo.FacultyDivisions");
            DropTable("dbo.FacultyCourses");
            DropTable("dbo.Faculties");
            DropTable("dbo.Divisions");
            DropTable("dbo.DivisionCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.Admins");
        }
    }
}
