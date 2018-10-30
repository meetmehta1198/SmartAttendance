using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class Division
    {
        public enum DivisionType 
        {
            C1,C2,C3,C4,C5,C6,C7,D1,D2,D3,D4,D5,D6,D7
        }
        [Key]
        public int DivisionId { get; set; }
        public DivisionType DivisionName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Faculty> Faculties { get; set; }
        public virtual ICollection<FacultyDivision> facultyDivision { get; set; }
        public virtual ICollection<DivisionCourse> divisionCourse { get; set; }
        //public virtual ICollection<StudentAttendance> studentAttendance { get; set; }
    }
}