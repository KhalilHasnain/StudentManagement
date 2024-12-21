using StudentManagement.ViewModels.PagingViewModel;
using StudentManagement.ViewModels.ResultViewModels;
using StudentManagement.ViewModels.StudentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.StudentServices
{
    public interface IStudentService
    {
        Task<int> AddStudentAsync(CreateStudentVM createStudentVM);
        IEnumerable<StudentsVM> GetAll();
        bool SetGroupIdToStudent(GroupStudentVM groupStudentVM);
        bool SetExamResult(StudentAttendExamVM studentAttendExamVM);
        IEnumerable<ResultVM> GetExamResults(int stdId);
        PagingInfoVM<GetStudentVM> GetAllStudents(int pageNumber, int pageSize);
        GetStudentProfileVM GetStudentById(int stdId);
        void UpdateStudentProfile(GetStudentProfileVM getStudentProfileVM);

        //My Logic
        IEnumerable<StudentsVM> GetAllStudentsExceptThatGroup(int grpId);
    }
}
