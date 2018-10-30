using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Attendance_ManagementV1._0.Models
{
    public enum FGender
    {
        Male,
        Female
    };
    public class Faculty
    {
        [Key]
        public int FacultyId { get; set; } // It is Username For Login
        public string FacultyName { get; set; }
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public FGender Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public String Mobilenumber { get; set; }


        public virtual ICollection<StudentFaculty> studentfaculties { get; set; }
        public virtual ICollection<FacultyDivision> facultyDivision { get; set; }
        public virtual ICollection<FacultyCourse> facultyCourse { get; set; }
    }
}