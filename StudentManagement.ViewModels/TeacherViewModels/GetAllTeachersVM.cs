using StudentManagement.Domain;
using StudentManagement.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.TeacherViewModels
{
    public class GetAllTeachersVM
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        //public string Role { get; set; }

        public GetAllTeachersVM(User user)
        {
            TeacherId= user.Id;
            Name = user.Name;
            UserName = user.UserName;
            EnumRole enumRole = (EnumRole)user.Role;
            //Role = enumRole.ToString();
        }
    }
}
