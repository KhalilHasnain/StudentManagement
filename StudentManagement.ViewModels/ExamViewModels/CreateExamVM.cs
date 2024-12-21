using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.ExamViewModels
{
    public class CreateExamVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupId { get; set; }

        public Exam ConvertToModel(CreateExamVM model)
        {
            return new Exam
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                Time = model.Time,
                GroupId = model.GroupId,
            };
        }
    }
}
