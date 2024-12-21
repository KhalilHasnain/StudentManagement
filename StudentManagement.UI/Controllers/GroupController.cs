using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.GroupServices;
using StudentManagement.BLL.StudentServices;
using StudentManagement.UI.CustomFilters;
using StudentManagement.ViewModels.GroupViewModels;
using StudentManagement.ViewModels.StudentViewModels;

namespace StudentManagement.UI.Controllers
{
    [RoleAuthorize(1)]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IStudentService _studentService;

        public GroupController(IGroupService groupService, IStudentService studentService)
        {
            _groupService = groupService;
            _studentService = studentService;
        }

        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            return View(_groupService.GetAll(pageNumber,pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(GetAllGroupsVM getAllGroupsVM)
        {
            var vm = _groupService.AddGroup(getAllGroupsVM);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //    GroupStudentVM groupStudentVM = new GroupStudentVM();
        //    var group = _groupService.GetGroup(id);
        //    var students = _studentService.GetAll();
        //    groupStudentVM.GroupId = group.Id;
        //    foreach (var student in students)
        //    {
        //        //groupStudentVM.StudentList.Add(new CheckBoxTable
        //        //{
        //        //    Id = student.Id,
        //        //    Name = student.Name,
        //        //    IsChecked = false
        //        //});
        //    }
        //    return View(groupStudentVM);
        //}

        [HttpGet]
        public IActionResult Details(int id)
        {
            GroupStudentVM groupStudentVM = new GroupStudentVM();
            var group = _groupService.GetGroup(id);
            var students = _studentService.GetAllStudentsExceptThatGroup(id);
            groupStudentVM.GroupId = group.Id;
            foreach (var student in students)
            {
                bool isInGroup = (student.GroupId == null) ? false : true;
                groupStudentVM.StudentList.Add(new CheckBoxTable
                {
                    Id = student.Id,
                    Name = student.Name,
                    IsChecked = isInGroup
                });
            }
            return View(groupStudentVM);
        }

        [HttpPost]
        public IActionResult Details(GroupStudentVM groupStudentVM)
        {
            bool success = _studentService.SetGroupIdToStudent(groupStudentVM);
            if (success)
            {
                return RedirectToAction("Index");
            }
            return View(groupStudentVM);
        }
    }
}
