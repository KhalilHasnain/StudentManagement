using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagement.BLL.ExamServices;
using StudentManagement.BLL.GroupServices;
using StudentManagement.UI.CustomFilters;
using StudentManagement.ViewModels.ExamViewModels;

namespace StudentManagement.UI.Controllers
{
    [RoleAuthorize(2)]
    public class ExamController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IExamService _examService;

        public ExamController(IGroupService groupService, IExamService examService)
        {
            _groupService = groupService;
            _examService = examService;
        }


        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            return View(_examService.GetAll(pageNumber,pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var groups = _groupService.GetAll();
            ViewBag.AllGroups = new SelectList(groups, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateExamVM createExamVM)
        {
            _examService.AddExam(createExamVM);
            return RedirectToAction("Index");
        }
    }
}
