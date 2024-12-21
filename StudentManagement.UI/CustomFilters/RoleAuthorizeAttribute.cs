using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StudentManagement.ViewModels.UserViewModels;

namespace StudentManagement.UI.CustomFilters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly int _roleId;

        public RoleAuthorizeAttribute(int roleId)
        {
            _roleId = roleId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionObj = context.HttpContext.Session.GetString("loginDetail");
            if (string.IsNullOrEmpty(sessionObj))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }
            else
            {
                var loginDetail = JsonConvert.DeserializeObject<UserLoginVM>(sessionObj);
                if (loginDetail.Role != _roleId)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
