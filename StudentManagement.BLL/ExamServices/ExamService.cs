using StudentManagement.Data.Unitofwork;
using StudentManagement.Domain;
using StudentManagement.ViewModels.ExamViewModels;
using StudentManagement.ViewModels.PagingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.ExamServices
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddExam(CreateExamVM createExamVM)
        {
            try
            {
                var model = createExamVM.ConvertToModel(createExamVM);
                _unitOfWork.GenericRepository<Exam>().Add(model);
                _unitOfWork.SaveAllChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PagingInfoVM<GetExamVM> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetExamVM> getExamVMs = new List<GetExamVM>();
                var examList = _unitOfWork.GenericRepository<Exam>().GetAll(includeEntities: "Group").Skip(excludedRecords).Take(pageSize).ToList();
                getExamVMs = ListInfo(examList);
                var result = new PagingInfoVM<GetExamVM>
                {
                    Data = getExamVMs,
                    TotalItems = _unitOfWork.GenericRepository<Exam>().GetAll().Count(),
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

        public IEnumerable<GetExamVM> GetAllExam()
        {
            List<GetExamVM> getExamVMs = new List<GetExamVM>();
            var exams = _unitOfWork.GenericRepository<Exam>().GetAll(includeEntities:"Group").ToList();
            getExamVMs = ListInfo(exams);
            return getExamVMs;
        }

        public IEnumerable<GetExamVM> GetAllExamByGroupId(int grpId)
        {
            List<GetExamVM> getExamVMs = new List<GetExamVM>();
            var exams = _unitOfWork.GenericRepository<Exam>().GetAll(x=>x.GroupId == grpId,null,includeEntities:"Group").ToList();
            getExamVMs = ListInfo(exams);
            return getExamVMs;
        }

        private List<GetExamVM> ListInfo(List<Exam> examList)
        {
            return examList.Select(x => new GetExamVM(x)).ToList();
        }
    }
}
