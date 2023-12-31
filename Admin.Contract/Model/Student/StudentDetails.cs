using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Student
{
    public class StudentDetails
    {
        public int ID { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string StudentEmail { get; set; }
        public string StudentPhone { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int PostalCode { get; set; } = 0;
        public string FathersName { get; set; } = string.Empty;
        public string MothersName { get; set; } = string.Empty;
        public string GurdianName { get; set; } = string.Empty;
        public string EmergencyContact { get; set; } = string.Empty;
        public Int32 Natinality { get; set; }
        public string StudentBloodGroup { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string Action { get; set; } = string.Empty;
        public string FileInput { get; set; } = string.Empty;
    }
}
