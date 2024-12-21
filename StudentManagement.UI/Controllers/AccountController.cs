using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.AccountService;
using StudentManagement.UI.FluentValidators;
using StudentManagement.ViewModels.UserViewModels;
using System;
using System.Security.Claims;
using System.Text.Json;

namespace StudentManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        //private readonly IValidator<UserLoginVM> _userLoginValidator;

        //public AccountController(IAccountService accountService, IValidator<UserLoginVM> userLoginValidator)
        //{
        //    _accountService = accountService;
        //    _userLoginValidator = userLoginValidator;
        //}
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userLoginVM)
        {
            //With Data Annotations

            if (ModelState.IsValid)
            {
                UserLoginVM userLogin = _accountService.Login(userLoginVM);
                if (userLogin != null)
                {
                    string sessionObj = JsonSerializer.Serialize(userLogin);
                    HttpContext.Session.SetString("loginDetail", sessionObj);

                    //implementing claim-based(cookie) authentication
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, userLoginVM.UserName)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    TempData["loginSuccess"] = "You are Sign In Successfully...!";

                    return RedirectToUser(userLogin);
                }
                ViewData["loginFailedHeading"] = "Login Attempt Failed";
                ViewData["loginFailedMessage"] = "Invalid Credentials...!";
                return View(userLoginVM);
            }
            return View(userLoginVM);



            //With Fluent Validation
            //var validationResult = await _userLoginValidator.ValidateAsync(userLoginVM);
            //if (!validationResult.IsValid)
            //{
            //    ModelState.Clear();
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return View(userLoginVM);
            //}
            //UserLoginVM userLogin = _accountService.Login(userLoginVM);
            //if (userLogin != null)
            //{
            //    string sessionObj = JsonSerializer.Serialize(userLogin);
            //    HttpContext.Session.SetString("loginDetail", sessionObj);

            //    //implementing claim-based(cookie) authentication
            //    var claims = new List<Claim>()
            //        {
            //            new Claim(ClaimTypes.Name, userLoginVM.UserName)
            //        };
            //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            //    return RedirectToUser(userLogin);
            //}
            //return View(userLoginVM);
        }

        private IActionResult RedirectToUser(UserLoginVM userLogin)
        {
            if (userLogin.Role == Convert.ToInt32(EnumRole.Admin))
            {
                //TempData["loginSuccess"] = "You are Sign In Successfully...!";
                return RedirectToAction("Index", "User");
            }
            else if (userLogin.Role == Convert.ToInt32(EnumRole.Teacher))
            {
                //TempData["loginSuccess"] = "You are Sign In Successfully...!";
                return RedirectToAction("Index", "Exam");
            }
            else
            {
                //TempData["loginSuccess"] = "You are Sign In Successfully...!";
                return RedirectToAction("Profile", "Student");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["logoutMessage"] = "You Are Sign Out Successfully...!";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
