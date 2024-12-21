using StudentManagement.ViewModels.PagingViewModel;
using StudentManagement.ViewModels.TeacherViewModels;
using StudentManagement.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.AccountService
{
    public interface IAccountService
    {
        bool AddNewTeacher(CreateUserVM createUserVM);
        UserLoginVM Login(UserLoginVM loginVM);
        PagingInfoVM<GetAllTeachersVM> GetAllTeachers(int pageNumber, int pageSize);
        PagingInfoVM<GetAllTeachersVM> SearchTeachers(string searchQuery, int pageNumber, int pageSize);
        EditUserProfileVM EditUserProfile(int userId);
        void EditUserProfile(EditUserProfileVM editUserProfileVM);
        void DeleteUserProfile(int userId);

        TeacherEditVM GetSingleTeacher(int userId);
    }
}
