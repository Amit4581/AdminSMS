using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Attendence
{

    public class StudentAttendenceLog
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string StudentId { get; set; }

        [StringLength(50)]
        public string StudentName { get; set; }

        public string ClassId { get; set; }
        public string ClassName { get; set; }

        public string SectionId { get; set; }
        public string SectionName { get; set; }

        public int? LeaveYear { get; set; }

        public int? Month { get; set; }

        public string Status { get; set; }

        public DateTime? AtDate { get; set; }

        public TimeSpan? InTime { get; set; }  // Change this line

        public TimeSpan? OutTime { get; set; }

        [StringLength(500)]
        public string LeaveType { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? ShiftFrom { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan? ShiftTo { get; set; }
    }


}

