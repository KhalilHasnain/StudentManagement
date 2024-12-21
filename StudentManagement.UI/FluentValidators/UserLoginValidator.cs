using FluentValidation;
using StudentManagement.ViewModels.UserViewModels;

namespace StudentManagement.UI.FluentValidators
{
    //public class UserLoginValidator : AbstractValidator<UserLoginVM>
    //{
    //    public UserLoginValidator()
    //    {
    //        RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is required.")
    //            .MinimumLength(3).WithMessage("Username must be at least 3 characters long!");
    //        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
    //            .MinimumLength(8).WithMessage("Password must be at least 8 characters long!");
    //        RuleFor(x => x.Role).NotEmpty().WithMessage("Please select a role.");
    //    }
    //}

    public class UserLoginValidator
    {
        
    }
}
