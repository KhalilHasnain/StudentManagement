using Microsoft.AspNetCore.Http;
using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.StudentViewModels
{
    public class GetStudentProfileVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? Contact { get; set; }
        public string? ResumeUrl { get; set; }
        public IFormFile ResumeFile { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public IFormFile ProfilePictureFile { get; set; }

        public GetStudentProfileVM()
        {
                
        }

        public GetStudentProfileVM(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            UserName = student.UserName;
            Contact = student.Contact;
            ResumeUrl = student.ResumeFileName;
            ProfilePictureUrl = student.ProfilePicture;
        }
    }
}
