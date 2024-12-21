using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.StudentViewModels
{
    public class GetStudentVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string UserName { get; set; }
        public string? Contact { get; set; }

        public GetStudentVM(Student student)
        {
            StudentId=student.Id;
            StudentName=student.Name;
            UserName=student.UserName;
            Contact=student.Contact;
        }
    }
}
