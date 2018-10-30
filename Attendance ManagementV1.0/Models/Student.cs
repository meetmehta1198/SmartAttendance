using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public enum SGender
    {
        Male,
        Female
    };
    public class Student
    {
        [Key]
        public int StudentId { get; set; } // It is Username For Login
        public string StudentName { get; set; }
        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public SGender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Branch { get; set; }
        public int Sem { get; set; }
        // public string Div { get; set; }

        [ForeignKey("Div")]
        public int DivisionId { get; set; }
     
        public virtual Division Div { get; set; }
        public virtual ICollection<StudentFaculty> studentfaculties { get; set; }
        public virtual ICollection<StudentCourse> studentCourse { get; set; }
        public virtual ICollection<StudentAttendance> studentAttendance { get; set; }
    }
}