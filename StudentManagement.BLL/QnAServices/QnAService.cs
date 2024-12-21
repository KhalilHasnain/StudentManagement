using StudentManagement.Data.Unitofwork;
using StudentManagement.Domain;
using StudentManagement.ViewModels.PagingViewModel;
using StudentManagement.ViewModels.QnAViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.QnAServices
{
    public class QnAService : IQnAService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QnAService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddQnA(CreateQnAVM createQnAVM)
        {
            try
            {
                var model = createQnAVM.ConvertToModel(createQnAVM);
                _unitOfWork.GenericRepository<QnA>().Add(model);
                _unitOfWork.SaveAllChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PagingInfoVM<GetQnAVM> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetQnAVM> getQnAVMs = new List<GetQnAVM>();
                var groupList = _unitOfWork.GenericRepository<QnA>().GetAll(includeEntities:"Exam").Skip(excludedRecords).Take(pageSize).ToList();
                getQnAVMs = ListInfo(groupList);
                var result = new PagingInfoVM<GetQnAVM>
                {
                    Data = getQnAVMs,
                    TotalItems = _unitOfWork.GenericRepository<QnA>().GetAll().Count(),
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<GetQnAVM> GetAllExamById(int examId)
        {
            var QnAs = _unitOfWork.GenericRepository<QnA>().GetAll().Where(x=>x.ExamId == examId).ToList();
            return ListInfo(QnAs);
        }

        public bool IsAttendExam(int examId, int studentId)
        {
            var result = _unitOfWork.GenericRepository<ExamResult>().GetAll().Any(x=>x.ExamId == examId && x.StudentId == studentId);
            return result == false ? false : true;
        }

        private List<GetQnAVM> ListInfo(List<QnA> groupList)
        {
            return groupList.Select(x=> new GetQnAVM(x)).ToList();
        }
    }
}
