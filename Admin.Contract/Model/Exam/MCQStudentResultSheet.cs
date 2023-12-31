using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Exam
{
    public class MCQStudentResultSheet
    {
        public int Id { get; set; }
        public string COMP_CODE { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }
        public string ClassId { get; set; }
        public string CourseName { get; set; }
        public string QsId { get; set; }
        public string QsNo { get; set; }
        public string Qs { get; set; }
        public string AnsGiven { get; set; }
        public string CorrectAns { get; set; }
        public string MarkObtain { get; set; }
        public DateTime EntryDate { get; set; }
        public int Active { get; set; }
        public string Action { get; set; }
    }
}
