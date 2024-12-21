using StudentManagement.ViewModels.QnAViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.StudentViewModels
{
    public class StudentAttendExamVM
    {
        public int StudentId { get; set; }
        public string ExamName { get; set; }
        public List<GetQnAVM> QnAsList { get; set; } = new();
        public string Message { get; set; }
    }
}
