using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.StudentViewModels
{
    public class CreateStudentVM
    {
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }

        public Student ConvertToModel(CreateStudentVM createStudentVM)
        {
            return new Student
            {
                Name = createStudentVM.Name,
                UserName = createStudentVM.UserName,
                Password = createStudentVM.Password,
            };
        }
    }
}
