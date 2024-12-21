using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.UserViewModels
{
    public class EditUserProfileVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Username is Required")]
        [Display(Name ="Username")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public EditUserProfileVM()
        {

        }

        public EditUserProfileVM(User user)
        {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            Password = "";
        }
    }
}
