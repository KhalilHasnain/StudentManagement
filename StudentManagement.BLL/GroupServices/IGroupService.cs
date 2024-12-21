using StudentManagement.ViewModels.GroupViewModels;
using StudentManagement.ViewModels.PagingViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.GroupServices
{
    public interface IGroupService
    {
        PagingInfoVM<GetAllGroupsVM> GetAll(int pageNumber, int pageSize);
        IEnumerable<GetAllGroupsVM> GetAll();
        GetAllGroupsVM GetGroup(int id);
        GetAllGroupsVM AddGroup(GetAllGroupsVM groupVM);
    }
}
