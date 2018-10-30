using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}