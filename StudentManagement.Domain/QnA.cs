using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain
{
    public class QnA
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        //comment because we cannot solve cascade cycle in fluent api
        //public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

        //uncomment because we can solve cascade cycle in fluent api
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}
