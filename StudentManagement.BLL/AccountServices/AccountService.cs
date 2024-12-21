using Microsoft.AspNetCore.Http;
using StudentManagement.Data.Unitofwork;
using StudentManagement.Domain;
using StudentManagement.ViewModels.PagingViewModel;
using StudentManagement.ViewModels.TeacherViewModels;
using StudentManagement.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AddNewTeacher(CreateUserVM createUserVM)
        {
            try
            {
                User user = new User()
                {
                    Name = createUserVM.Name,
                    UserName = createUserVM.UserName,
                    Password = createUserVM.Password,
                    Role = Convert.ToInt32(EnumRole.Teacher)
                };
                _unitOfWork.GenericRepository<User>().Add(user);
                _unitOfWork.SaveAllChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }

        public PagingInfoVM<GetAllTeachersVM> GetAllTeachers(int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetAllTeachersVM> getAllTeachersVMs= new List<GetAllTeachersVM>();
                var userList = _unitOfWork.GenericRepository<User>().GetAll().Where(x => x.Role == Convert.ToInt32(EnumRole.Teacher)).Skip(excludedRecords).Take(pageSize).ToList();
                getAllTeachersVMs = ListInfo(userList);
                var result = new PagingInfoVM<GetAllTeachersVM>
                {
                    Data = getAllTeachersVMs,
                    TotalItems = _unitOfWork.GenericRepository<User>().GetAll().Where(x => x.Role == Convert.ToInt32(EnumRole.Teacher)).Count(),
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public PagingInfoVM<GetAllTeachersVM> SearchTeachers(string searchQuery, int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetAllTeachersVM> getAllTeachersVMs = new List<GetAllTeachersVM>();
                var userList = _unitOfWork.GenericRepository<User>().GetAll().Where(x => x.Role == Convert.ToInt32(EnumRole.Teacher) && (x.Name.Contains(searchQuery) || x.UserName.Contains(searchQuery))).Skip(excludedRecords).Take(pageSize).ToList();
                getAllTeachersVMs = ListInfo(userList);
                var result = new PagingInfoVM<GetAllTeachersVM>
                {
                    Data = getAllTeachersVMs,
                    TotalItems = _unitOfWork.GenericRepository<User>().GetAll().Where(x => x.Role == Convert.ToInt32(EnumRole.Teacher)).Count(),
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<GetAllTeachersVM> ListInfo(List<User> userList)
        {
            return userList.Select(x=> new GetAllTeachersVM(x)).ToList();
        }

        public UserLoginVM Login(UserLoginVM loginVM)
        {
            if (loginVM.Role == Convert.ToInt32(EnumRole.Admin) || loginVM.Role == Convert.ToInt32(EnumRole.Teacher))
            {
                var user = _unitOfWork.GenericRepository<User>().GetAll().FirstOrDefault(x => x.UserName == loginVM.UserName.Trim() && x.Password == loginVM.Password && x.Role == loginVM.Role);
                if (user != null)
                {
                    loginVM.Id = user.Id;
                    loginVM.Name = user.Name;
                    return loginVM;
                }
            }
            else
            {
                var user = _unitOfWork.GenericRepository<Student>().GetAll().FirstOrDefault(x => x.UserName == loginVM.UserName.Trim() && x.Password == loginVM.Password);
                if (user != null)
                {
                    loginVM.Id = user.Id;
                    loginVM.Name = user.Name;
                    loginVM.GrpId = user.GroupId;
                    return loginVM;
                }
            }
            return null;


            //var user = _unitOfWork.GenericRepository<User>().GetAll().FirstOrDefault(x => x.UserName == loginVM.UserName.Trim() && x.Password == loginVM.Password && x.Role == loginVM.Role);
            //if (user != null)
            //{
            //    loginVM.Id = user.Id;
            //    return loginVM;
            //}
            //return null;
        }

        public EditUserProfileVM EditUserProfile(int userId)
        {
            try
            {
                var userFromDB = _unitOfWork.GenericRepository<User>().GetById(userId);
                EditUserProfileVM userProfile = new EditUserProfileVM(userFromDB);
                return userProfile;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void EditUserProfile(EditUserProfileVM editUserProfileVM)
        {
            User user = _unitOfWork.GenericRepository<User>().GetById(editUserProfileVM.Id);
            user.Name = editUserProfileVM.Name;
            user.UserName = editUserProfileVM.UserName;
            user.Password = editUserProfileVM.Password;
            _unitOfWork.GenericRepository<User>().Update(user);
            _unitOfWork.SaveAllChanges();
        }

        public void DeleteUserProfile(int userId)
        {
            _unitOfWork.GenericRepository<User>().DeleteById(userId);
            _unitOfWork.SaveAllChanges();
        }

        public TeacherEditVM GetSingleTeacher(int userId)
        {
            try
            {
                var userFromDB = _unitOfWork.GenericRepository<User>().GetById(userId);
                TeacherEditVM userProfile = new TeacherEditVM(userFromDB);
                return userProfile;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
