using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.ResultViewModels
{
    public class ResultVM
    {
        public int StudentId { get; set; }
        public string ExamName { get; set; }
        public int TotalQuestion { get; set; }
        public int CorrectAnswer { get; set; }
        public int WrongAnswer { get; set; }
    }
}
