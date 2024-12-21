using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.ExamViewModels
{
    public class GetExamVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        //public int GroupId { get; set; }
        public string GroupName { get; set; }

        public GetExamVM(Exam exam)
        {
            Id = exam.Id;
            Title = exam.Title;
            Description = exam.Description;
            StartDate = exam.StartDate;
            Time = exam.Time;
            GroupName = exam.Group.Name;
        }
    }
}
