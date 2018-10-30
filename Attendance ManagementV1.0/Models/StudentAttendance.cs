using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class StudentAttendance
    {
        [Key]
        public int StudentAttendenceId { get; set;}
        public int Status { get; set; }
        [ForeignKey("courses")]
        public int CourseId { get; set; }

        public DateTime date { get; set; }

        [ForeignKey("students")]
        public int StudentId { get; set; }
       // [ForeignKey("divisions")]
        public int DivisonId { get; set; }

       public virtual  Course courses { get; set; }
        public virtual Student students { get; set; }
       // public virtual Division divisions { get; set; }


    }
}