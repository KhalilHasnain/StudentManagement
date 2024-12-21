using StudentManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.ViewModels.GroupViewModels
{
    public class GetAllGroupsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GetAllGroupsVM()
        {
        }

        public GetAllGroupsVM(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            Description = group.Description;
        }


        public Group ConvertToGroup(GetAllGroupsVM vm)
        {
            return new Group
            {
                Id = vm.Id,
                Name = vm.Name,
                Description = vm.Description,
            };
        }
    }
}
