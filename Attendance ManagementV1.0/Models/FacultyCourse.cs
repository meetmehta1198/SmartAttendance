using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class FacultyCourse
    {
        [Key]
        [Column(Order = 1)]
        public int FacultyId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CourseId { get; set; }
        [Key]
        [Column(Order =3)]
        public int DivisionId { get; set; }
        public virtual Faculty faculties { get; set; }
        public virtual Course courses { get; set; }
        
    }
}