using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.UserViewModels
{
    public class TeacherEditVM
    {
        public string Password { get; set; }

        public TeacherEditVM(User user)
        {
                Password = user.Password;
        }
    }
}
