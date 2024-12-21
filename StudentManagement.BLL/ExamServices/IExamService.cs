using StudentManagement.ViewModels.ExamViewModels;
using StudentManagement.ViewModels.PagingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.ExamServices
{
    public interface IExamService
    {
        PagingInfoVM<GetExamVM> GetAll(int pageNumber, int pageSize);
        void AddExam(CreateExamVM createExamVM);
        IEnumerable<GetExamVM> GetAllExam();
        IEnumerable<GetExamVM> GetAllExamByGroupId(int grpId);
    }
}
