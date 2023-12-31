using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Teacher
{
    public class TeacherDetails
    {
    
        public int Id { get; set; }
        public string TeacherId { get; set; } = string.Empty;
        public string TeacherFName { get; set; } = string.Empty;
        public string TeacherLName { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }  
        public string Gender { get; set; } = string.Empty;
        public DateTime? JoiningDate { get; set; }  
        public string SubjectTaught { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public decimal? Salary { get; set; }    
        public string TeacherEmail { get; set; } = string.Empty;
        public string TeacherPhone { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;                
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? PostalCode { get; set; }
        public string FathersName { get; set; } = string.Empty;
        public string MothersName { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public bool? Natinality { get; set; }
        public string TeacherBloodGroup { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Imageupload { get; set; } = string.Empty;

        public int? IsActive { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
