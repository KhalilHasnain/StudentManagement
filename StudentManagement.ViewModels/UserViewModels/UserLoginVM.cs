using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.UserViewModels
{
    //With Data Annotations

    public class UserLoginVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [Display(Name ="Username")]
        public string UserName { get; set; }
        public string? Name { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select Role")]
        public int Role { get; set; }

        //MyLogic
        public int? GrpId { get; set; }
    }


    //With Fluent Validation
    //public class UserLoginVM
    //{
    //    public int Id { get; set; }
    //    public string UserName { get; set; }
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }
    //    public int Role { get; set; }

    //    //MyLogic
    //    public int? GrpId { get; set; }
    //}
}
