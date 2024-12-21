using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StudentManagement.BLL.ExamServices;
using StudentManagement.BLL.QnAServices;
using StudentManagement.BLL.StudentServices;
using StudentManagement.BLL.UtilityServices;
using StudentManagement.UI.CustomFilters;
using StudentManagement.ViewModels.StudentViewModels;
using StudentManagement.ViewModels.UserViewModels;

namespace StudentManagement.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;
        private readonly IUtilityService _utilityService;
        private string studentImageContainerName = "StudentImages";
        private string studentResumeContainerName = "StudentResumes";
        public StudentController(IStudentService studentService, IExamService examService, IQnAService qnAService, IUtilityService utilityService)
        {
            _studentService = studentService;
            _examService = examService;
            _qnAService = qnAService;
            _utilityService = utilityService;
        }
        //[RoleAuthorize(1)]
        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            return View(_studentService.GetAllStudents(pageNumber,pageSize));
        }

        //[RoleAuthorize(1)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[RoleAuthorize(1)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentVM createStudentVM)
        {
            var success = await _studentService.AddStudentAsync(createStudentVM);
            if (success > 0)
            {
                return RedirectToAction("Index");
            }
            return View(createStudentVM);
        }

        //[HttpGet]
        //public IActionResult AttendExam()
        //{
        //    StudentAttendExamVM studentAttendExamVM = new StudentAttendExamVM();
        //    string loginObj = HttpContext.Session.GetString("loginDetail");
        //    UserLoginVM sessionObj = JsonConvert.DeserializeObject<UserLoginVM>(loginObj);
        //    if (sessionObj != null)
        //    {
        //        studentAttendExamVM.StudentId = sessionObj.Id;
        //        var todayExam = _examService.GetAllExam().Where(x=>x.StartDate.Date == DateTime.Now.Date).FirstOrDefault();
        //        if (todayExam == null)
        //        {
        //            studentAttendExamVM.Message = "No Exam Scheduled Today...!";
        //            return View(studentAttendExamVM);
        //        }
        //        else
        //        {
        //            if (!_qnAService.IsAttendExam(todayExam.Id, studentAttendExamVM.StudentId))
        //            {
        //                studentAttendExamVM.QnAsList = _qnAService.GetAllExamById(todayExam.Id).ToList();
        //                studentAttendExamVM.ExamName = todayExam.Title;
        //                studentAttendExamVM.Message = "";
        //                return View(studentAttendExamVM);
        //            }
        //            else
        //            {
        //                studentAttendExamVM.Message = "You have already attend this exam...!";
        //                return View(studentAttendExamVM);
        //            }
        //        }
        //    }
        //    return RedirectToAction("Login","Account");
        //}

        [HttpGet]
        public IActionResult AttendExam()
        {
            StudentAttendExamVM studentAttendExamVM = new StudentAttendExamVM();
            string loginObj = HttpContext.Session.GetString("loginDetail");
            UserLoginVM sessionObj = JsonConvert.DeserializeObject<UserLoginVM>(loginObj);
            if (sessionObj != null)
            {
                studentAttendExamVM.StudentId = sessionObj.Id;
                var todayExam = _examService.GetAllExamByGroupId(sessionObj.GrpId.Value).Where(x => x.StartDate.Date == DateTime.Now.Date).FirstOrDefault();
                if (todayExam == null)
                {
                    studentAttendExamVM.Message = "No Exam Scheduled Today...!";
                    return View(studentAttendExamVM);
                }
                else
                {
                    if (!_qnAService.IsAttendExam(todayExam.Id, studentAttendExamVM.StudentId))
                    {
                        studentAttendExamVM.QnAsList = _qnAService.GetAllExamById(todayExam.Id).ToList();
                        studentAttendExamVM.ExamName = todayExam.Title;
                        studentAttendExamVM.Message = "";
                        return View(studentAttendExamVM);
                    }
                    else
                    {
                        studentAttendExamVM.Message = "You have already attend this exam...!";
                        return View(studentAttendExamVM);
                    }
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult AttendExam(StudentAttendExamVM studentAttendExamVM)
        {
            bool result = _studentService.SetExamResult(studentAttendExamVM);
            return RedirectToAction("Result");
        }

        public IActionResult Result()
        {
            var loginSessionObj = HttpContext.Session.GetString("loginDetail");
            if (loginSessionObj != null)
            {
                var loginViewModel = JsonConvert.DeserializeObject<UserLoginVM>(loginSessionObj);
                var model = _studentService.GetExamResults(Convert.ToInt32(loginViewModel.Id));
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetail");
            if (sessionObj != null)
            {
                var loginDetail = JsonConvert.DeserializeObject<UserLoginVM>(sessionObj);
                var studentDetail = _studentService.GetStudentById(loginDetail.Id);
                return View(studentDetail);
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public async Task<IActionResult> Profile(GetStudentProfileVM getStudentProfileVM)
        {
            if (getStudentProfileVM.ProfilePictureFile != null)
                getStudentProfileVM.ProfilePictureUrl = await _utilityService.SaveImage(studentImageContainerName, getStudentProfileVM.ProfilePictureFile);
            if (getStudentProfileVM.ResumeFile != null)
                getStudentProfileVM.ResumeUrl = await _utilityService.SaveImage(studentResumeContainerName, getStudentProfileVM.ResumeFile);
            _studentService.UpdateStudentProfile(getStudentProfileVM);
            return RedirectToAction("Profile","Student");
        }
    }
}
