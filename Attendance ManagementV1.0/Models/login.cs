using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public class login
    {
        public enum UserType
        {
            Student,
            Faculty,
            Admin,
        };
        
        public string username { get; set; }
        
        [DataType(DataType.Password)]
        public string password { get; set; }

        public UserType usertype { get; set; }
    }
}