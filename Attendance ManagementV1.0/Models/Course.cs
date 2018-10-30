using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Sem { get; set; }
        public virtual ICollection<FacultyCourse> facultyCourse { get; set; }
        public virtual ICollection<StudentCourse> studentCourse { get; set; }
      //  public virtual ICollection<FacultyDivision> facultyDivision { get; set; }
        // public virtual ICollection<AttendenceCourse> AttendenceCourses { get; set; }
        public virtual ICollection<DivisionCourse> divisionCourse { get; set; }
        public virtual ICollection<StudentAttendance> studentAttendance { get; set; }
    }
}