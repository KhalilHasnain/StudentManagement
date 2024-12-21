using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Data.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<QnA> QnAs { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamResult>(entity =>
            {
                //resolve multiple cascade paths
                entity.HasOne(a => a.Exam).WithMany(b => b.ExamResults).HasForeignKey(c => c.ExamId).HasConstraintName("FK_ExamResults_Exams_ExamId");
                entity.HasOne(a => a.QnA).WithMany(b => b.ExamResults).HasForeignKey(c => c.QnAId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExamResults_QnAs_QnAId");
                entity.HasOne(a => a.Student).WithMany(b => b.ExamResults).HasForeignKey(c => c.StudentId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExamResults_Students_StudentId");
            });

            //Database Seeding for Admin
            modelBuilder.Entity<User>().HasData(new User { Id=1, Name="Admin", Password="Admin@123", Role=1, UserName="admin"});
            base.OnModelCreating(modelBuilder);
        }
    }
}
