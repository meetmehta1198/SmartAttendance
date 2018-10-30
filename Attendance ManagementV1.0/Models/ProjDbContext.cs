namespace Attendance_ManagementV1._0.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProjDbContext : DbContext
    {
        // Your context has been configured to use a 'ProjDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Attendance_ManagementV1._0.Models.ProjDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProjDbContext' 
        // connection string in the application configuration file.
        public ProjDbContext()
            : base("name=ProjDbContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        //public DbSet<login> Logins { get; set; }
        public DbSet<StudentFaculty> StudentFaculties { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<FacultyDivision> FacultyDivisions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FacultyCourse> FacultyCourses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<StudentAttendance> StudentAttendances {get; set;}
        public DbSet<DivisionCourse>DivisionCourses { get; set; }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}