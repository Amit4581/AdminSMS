using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Exam
{

    public class MCQCourseDetail
    {
        public int Id { get; set; }
        public string COMP_CODE { get; set; }
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDesc { get; set; }
        public string ClassId { get; set; }
        public DateTime? Start_time { get; set; }
        public TimeSpan? Timeduration { get; set; }
        public int? TotalMark { get; set; }
        public int? PassMark { get; set; }
        public string CourseToken { get; set; }
        public int? MaxNoStudent { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? Active { get; set; }
        public string Action { get; set; }
    }
}
