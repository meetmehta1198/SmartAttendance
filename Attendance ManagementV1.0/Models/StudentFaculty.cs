using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class StudentFaculty
    {
        [Key]
        [Column(Order=1)]
        public int StudentId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int FacultyId { get; set; }

        public virtual Faculty faculty { get; set; }
        public virtual Student student { get; set; }
    }
}