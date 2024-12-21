using StudentManagement.Data.Unitofwork;
using StudentManagement.Domain;
using StudentManagement.ViewModels.GroupViewModels;
using StudentManagement.ViewModels.PagingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.GroupServices
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GetAllGroupsVM AddGroup(GetAllGroupsVM groupVM)
        {
            try
            {
                var group = groupVM.ConvertToGroup(groupVM);
                _unitOfWork.GenericRepository<Group>().Add(group);
                _unitOfWork.SaveAllChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return groupVM;
        }

        public PagingInfoVM<GetAllGroupsVM> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                int excludedRecords = (pageNumber * pageSize) - pageSize;
                List<GetAllGroupsVM> getAllGroupsVMs=new List<GetAllGroupsVM>();
                var groupList = _unitOfWork.GenericRepository<Group>().GetAll().Skip(excludedRecords).Take(pageSize).ToList();
                getAllGroupsVMs = ListInfo(groupList);
                var result = new PagingInfoVM<GetAllGroupsVM>()
                {
                    Data = getAllGroupsVMs,
                    TotalItems = _unitOfWork.GenericRepository<Group>().GetAll().Count(),
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

        private List<GetAllGroupsVM> ListInfo(List<Group> groupList)
        {
            return groupList.Select(x=> new GetAllGroupsVM(x)).ToList();
        }

        public IEnumerable<GetAllGroupsVM> GetAll()
        {
            try
            {
                List<GetAllGroupsVM> getAllGroupsVMs = new List<GetAllGroupsVM>();
                var groupList = _unitOfWork.GenericRepository<Group>().GetAll().ToList();
                getAllGroupsVMs = ListInfo(groupList);
                return getAllGroupsVMs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public GetAllGroupsVM GetGroup(int id)
        {
            var group = _unitOfWork.GenericRepository<Group>().GetById(id);
            GetAllGroupsVM getAllGroupsVM = new GetAllGroupsVM(group);
            return getAllGroupsVM;
        }
    }
}
