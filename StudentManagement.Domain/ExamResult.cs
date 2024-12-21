using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        //comment because we cannot solve cascade cycle in fluent api
        //public int QnAId { get; set; }
        //public virtual QnA QnA { get; set; }

        //uncomment because we can solve cascade cycle in fluent api
        public int QnAId { get; set; }
        public virtual QnA QnA { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public int Answer { get; set; }
    }
}
