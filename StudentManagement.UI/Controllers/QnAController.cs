using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.BLL.ExamServices;
using StudentManagement.BLL.QnAServices;
using StudentManagement.ViewModels.QnAViewModels;

namespace StudentManagement.UI.Controllers
{
    public class QnAController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQnAService _qnAService;

        public QnAController(IExamService examService, IQnAService qnAService)
        {
            _examService = examService;
            _qnAService = qnAService;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            return View( _qnAService.GetAll(pageNumber,pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            var exams = _examService.GetAllExam();
            ViewBag.AllExams = new SelectList(exams, "Id", "Title");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateQnAVM createQnAVM)
        {
            _qnAService.AddQnA(createQnAVM);
            return RedirectToAction("Index");
        }
    }
}
