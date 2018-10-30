using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class DivisionCourse
    {
        [Key]
        [Column(Order=1)]
        public int DivisionId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }
        public virtual Division divisions { get; set; }
        public virtual Course courses { get; set; }
    }
}