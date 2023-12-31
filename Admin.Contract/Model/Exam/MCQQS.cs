using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Exam
{
    public class MCQQS
    {
        public int Id { get; set; }
        public string Comp_Code { get; set; } = string.Empty;   
        public string CourseId { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;         
        public string QsId { get; set; } = string.Empty;
        public string QsNo { get; set; } = string.Empty;
        public string Qs { get; set; } = string.Empty;
        public string Op1 { get; set; } = string.Empty;
        public string Op2 { get; set; } = string.Empty;
        public string Op3 { get; set; } = string.Empty;
        public string Op4 { get; set; } = string.Empty;
        public string Ans { get; set; } = string.Empty;
        public decimal CorrectMarks { get; set; }
        public decimal WrongMarks { get; set; }
        public string Tag { get; set; } = string.Empty;
        public DateTime EntrydateTime { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
