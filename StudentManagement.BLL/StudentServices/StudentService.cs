using StudentManagement.Data.Unitofwork;
using StudentManagement.Domain;
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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddStudentAsync(CreateStudentVM createStudentVM)
        {
            try
            {
                Student student = createStudentVM.ConvertToModel(createStudentVM);
                await _unitOfWork.GenericRepository<Student>().AddAsyncReturn(student);
                _unitOfWork.SaveAllChanges();
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<StudentsVM> GetAll()
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Student>().GetAll().ToList();
                List<StudentsVM> studentsVMs = new List<StudentsVM>();
                studentsVMs = ListInfo(students);
                return studentsVMs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetGroupIdToStudent(GroupStudentVM groupStudentVM)
        {
            try
            {
                foreach (var item in groupStudentVM.StudentList)
                {
                    var student = _unitOfWork.GenericRepository<Student>().GetById(item.Id);
                    if (item.IsChecked)
                    {
                        student.GroupId = groupStudentVM.GroupId;
                        _unitOfWork.GenericRepository<Student>().Update(student);
                    }
                    else
                    {
                        student.GroupId = null;
                    }
                }
                _unitOfWork.SaveAllChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetExamResult(StudentAttendExamVM studentAttendExamVM)
        {
            try
            {
                foreach (var item in studentAttendExamVM.QnAsList)
                {
                    ExamResult examResult = new ExamResult();
                    examResult.StudentId = studentAttendExamVM.StudentId;
                    examResult.ExamId = item.ExamId;
                    examResult.QnAId = item.Id;
                    examResult.Answer = item.SelectedAnswer;
                    _unitOfWork.GenericRepository<ExamResult>().Add(examResult);
                }
                _unitOfWork.SaveAllChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public IEnumerable<ResultVM> GetExamResults(int stdId)
        {
            try
            {
                var examResults = _unitOfWork.GenericRepository<ExamResult>().GetAll().Where(x => x.StudentId == stdId);
                var students = _unitOfWork.GenericRepository<Student>().GetAll();
                var exams = _unitOfWork.GenericRepository<Exam>().GetAll();
                var qnas = _unitOfWork.GenericRepository<QnA>().GetAll();
                var requiredData = examResults.Join(students, er => er.StudentId, s => s.Id, (er, st) => new { er, st })
                    .Join(exams, erj => erj.er.ExamId, ex => ex.Id, (erj, ex) => new { erj, ex })
                    .Join(qnas, exj => exj.erj.er.QnAId, q => q.Id, (exj, q) =>
                        new ResultVM()
                        {
                            StudentId = stdId,
                            ExamName = exj.ex.Title,
                            TotalQuestion = examResults.Count(a => a.StudentId == stdId && a.ExamId == exj.ex.Id),
                            CorrectAnswer = examResults.Count(b => b.StudentId == stdId && b.ExamId == exj.ex.Id && b.Answer == q.Answer),
                            WrongAnswer = examResults.Count(c => c.StudentId == stdId && c.ExamId == exj.ex.Id && c.Answer != q.Answer)
                        }
                    );
                return requiredData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<StudentsVM> ListInfo(List<Student> students)
        {
            return students.Select(x => new StudentsVM(x)).ToList();
        }

        public PagingInfoVM<GetStudentVM> GetAllStudents(int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetStudentVM> getStudentVMs = new List<GetStudentVM>();
                var studentList = _unitOfWork.GenericRepository<Student>().GetAll().Skip(excludedRecords).Take(pageSize).ToList();
                getStudentVMs = ConvertToGetStudentVM(studentList);
                var result = new PagingInfoVM<GetStudentVM>()
                {
                    Data = getStudentVMs,
                    TotalItems = _unitOfWork.GenericRepository<Student>().GetAll().Count(),
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

        private List<GetStudentVM> ConvertToGetStudentVM(List<Student> students)
        {
            return students.Select(x => new GetStudentVM(x)).ToList();
        }

        public GetStudentProfileVM GetStudentById(int stdId)
        {
            var student = _unitOfWork.GenericRepository<Student>().GetById(stdId);
            var studentProfile = new GetStudentProfileVM(student);
            return studentProfile;
        }

        public void UpdateStudentProfile(GetStudentProfileVM getStudentProfileVM)
        {
            try
            {
                var student = _unitOfWork.GenericRepository<Student>().GetById(getStudentProfileVM.Id);
                if (student != null)
                {
                    student.Name = getStudentProfileVM.Name;
                    student.Contact = getStudentProfileVM.Contact;
                    student.ProfilePicture = getStudentProfileVM.ProfilePictureUrl != null ? getStudentProfileVM.ProfilePictureUrl : student.ProfilePicture;
                    student.ResumeFileName = getStudentProfileVM.ResumeUrl != null ? getStudentProfileVM.ResumeUrl : student.ResumeFileName;
                    _unitOfWork.GenericRepository<Student>().Update(student);
                    _unitOfWork.SaveAllChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        //My Logic
        public IEnumerable<StudentsVM> GetAllStudentsExceptThatGroup(int grpId)
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Student>().GetAll(filter: x=>x.GroupId == grpId || x.GroupId == null).ToList();
                List<StudentsVM> studentsVMs = new List<StudentsVM>();
                studentsVMs = ListInfo(students);
                return studentsVMs;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
