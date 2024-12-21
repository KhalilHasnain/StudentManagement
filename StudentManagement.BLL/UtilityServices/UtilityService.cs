using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.BLL.UtilityServices
{
    //public class UtilityService
    //{
    //}
    public class UtilityService : IUtilityService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        public UtilityService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> SaveImage(string containerName, IFormFile formFile)
        {
            //getting image extension
            var imageExtension = Path.GetExtension(formFile.FileName);
            //generating unique image name
            var imageName = $"{Guid.NewGuid()}{imageExtension}";
            //specifying folder name where images are stored
            string folderName = Path.Combine(_webHostEnvironment.WebRootPath, containerName);
            //checking if folder is exists or not, if exists then ok if not exists then create a folder
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
            //creating full image path
            string imagePath = Path.Combine(folderName, imageName);
            //storing image in folder
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(imagePath, content);
            }
            var basePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            var completePath = Path.Combine(basePath, containerName, imageName).Replace("\\", "/");
            //https:localhost:87634/ContainerName/jkhfi-8fjh3-h02bc-o3bk8c.jpg
            return completePath;
        }

        public async Task<string> EditImage(string containerName, IFormFile formFilem, string imageDBPath)
        {
            await DeleteImage(containerName, imageDBPath);
            return await SaveImage(containerName, formFilem);
        }

        public Task DeleteImage(string containerName, string imageDBPath)
        {
            if (string.IsNullOrEmpty(imageDBPath))
            {
                return Task.CompletedTask;
            }
            var imageName = Path.GetFileName(imageDBPath);
            var completePath = Path.Combine(_webHostEnvironment.WebRootPath, containerName, imageName);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
            }
            return Task.CompletedTask;
        }
    }
}
