using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.StudentViewModels
{
    public class StudentsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? GroupId { get; set; }

        public StudentsVM(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            GroupId = student.GroupId;
        }
    }
    public class CheckBoxTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
