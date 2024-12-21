using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.UtilityServices
{
    public interface IUtilityService
    {
        Task<string> SaveImage(string containerName, IFormFile formFile);
        Task<string> EditImage(string containerName, IFormFile formFilem, string imageDBPath);
        Task DeleteImage(string containerName, string imageDBPath);
    }
}
