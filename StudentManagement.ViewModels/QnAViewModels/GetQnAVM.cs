using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.QnAViewModels
{
    public class GetQnAVM
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public int SelectedAnswer { get; set; }

        public GetQnAVM()
        {
                
        }
        public GetQnAVM(QnA qnA)
        {
            Id=qnA.Id;
            QuestionTitle = qnA.QuestionTitle;
            ExamId = qnA.ExamId;
            ExamName = qnA.Exam.Title;
            Answer = qnA.Answer;
            Option1 = qnA.Option1;
            Option2 = qnA.Option2;
            Option3 = qnA.Option3;
            Option4 = qnA.Option4;
        }
    }
}
