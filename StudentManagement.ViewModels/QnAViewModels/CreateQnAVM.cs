using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.QnAViewModels
{
    public class CreateQnAVM
    {
        public string QuestionTitle { get; set; }
        public int ExamId { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public QnA ConvertToModel(CreateQnAVM createQnAVM)
        {
            return new QnA
            {
                QuestionTitle = createQnAVM.QuestionTitle,
                ExamId=createQnAVM.ExamId,
                Answer=createQnAVM.Answer,
                Option1=createQnAVM.Option1,
                Option2=createQnAVM.Option2,
                Option3=createQnAVM.Option3,
                Option4=createQnAVM.Option4
            };
        }
    }
}
