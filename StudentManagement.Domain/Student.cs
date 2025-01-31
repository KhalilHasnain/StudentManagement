﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Contact { get; set; }
        public string? ResumeFileName { get; set; }
        public string? ProfilePicture { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; }
    }
}
