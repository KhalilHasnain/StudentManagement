using StudentManagement.ViewModels.PagingViewModel;
using StudentManagement.ViewModels.QnAViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.QnAServices
{
    public interface IQnAService
    {
        void AddQnA(CreateQnAVM createQnAVM);
        PagingInfoVM<GetQnAVM> GetAll(int pageNumber, int pageSize);
        bool IsAttendExam(int examId, int studentId);
        IEnumerable<GetQnAVM> GetAllExamById(int examId);
    }
}
