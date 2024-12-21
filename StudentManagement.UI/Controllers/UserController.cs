using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartBreadcrumbs.Attributes;
using StudentManagement.BLL.AccountService;
using StudentManagement.UI.CustomFilters;
using StudentManagement.ViewModels.UserViewModels;

namespace StudentManagement.UI.Controllers
{
    [RoleAuthorize(1)]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        //without search functionality
        //[HttpGet]
        //public IActionResult Index(int pageNumber = 1, int pageSize = 7)
        //{
        //    return View(_accountService.GetAllTeachers(pageNumber,pageSize));
        //}

        //with search functionality
        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 7, string searchQuery = "")
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchResult = _accountService.SearchTeachers(searchQuery, pageNumber, pageSize);
                return View(searchResult);
            }
            else
            {
                return View(_accountService.GetAllTeachers(pageNumber, pageSize));
            }
            //return View(_accountService.GetAllTeachers(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserVM createUserVM)
        {
            bool success = _accountService.AddNewTeacher(createUserVM);
            if (success)
            {
                TempData["teacherCreate"] = "Teacher Profile is Created Successfully...!";
                return RedirectToAction("Index");
            }
            return View(createUserVM);
        }

        [HttpGet]
        public IActionResult AdminProfile()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetail");
            var loginDetails = JsonConvert.DeserializeObject<UserLoginVM>(sessionObj);
            EditUserProfileVM editUserProfileVM = _accountService.EditUserProfile(loginDetails.Id);
            return View(editUserProfileVM);
        }

        [HttpPost]
        public IActionResult AdminProfile(EditUserProfileVM editUserProfileVM)
        {
            if (editUserProfileVM.Password == null)
            {
                var sessionObj = HttpContext.Session.GetString("loginDetail");
                var loginDetails = JsonConvert.DeserializeObject<UserLoginVM>(sessionObj);
                editUserProfileVM.Password = loginDetails.Password;
            }
            _accountService.EditUserProfile(editUserProfileVM);
            TempData["adminProfile"] = "Profile Updated Successfully...!";
            return RedirectToAction("AdminProfile", "User");
        }
        #region API Call
        public IActionResult Delete(int teacherId)
        {
            _accountService.DeleteUserProfile(teacherId);
            return Json(new { success = true, title= "Deleted!", message = "Teacher Profile has been deleted." });
        }
        #endregion

        [HttpGet]
        public IActionResult EditTeacher(int teacherId)
        {
            EditUserProfileVM editUserProfileVM = _accountService.EditUserProfile(teacherId);
            return View(editUserProfileVM);
        }

        [HttpPost]
        public IActionResult EditTeacher(EditUserProfileVM editUserProfileVM)
        {
            if (editUserProfileVM.Password == null)
            {
                var teacher = _accountService.GetSingleTeacher(editUserProfileVM.Id);
                editUserProfileVM.Password = teacher.Password;
            }
            _accountService.EditUserProfile(editUserProfileVM);
            TempData["adminProfile"] = "Profile Updated Successfully...!";
            return RedirectToAction("Index", "User");
        }
    }
}
