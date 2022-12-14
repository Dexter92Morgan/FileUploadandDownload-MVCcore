using FileUploadandDownloadMVCcore.Models;
using FileUploadandDownloadMVCcore.SpecialClass;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace FileUploadandDownloadMVCcore.Controllers
{
    
    public class UploadtofolderController : Controller
    {
        private IHostingEnvironment Environment;

        public UploadtofolderController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult Index()
        {
            return View();
        }


    [HttpPost]
    public IActionResult Index(IFormFile files)
    {
        if (files != null)
        {
            if (files.Length > 0)
            {
                //Getting FileName
                var fileName = Path.GetFileName(files.FileName);

                //Assigning Unique Filename (Guid)
                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);

                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(myUniqueFileName, fileExtension);

                // Combines two strings into a path.
                var filepath =
        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images")).Root + $@"\{fileName}";// fileExtension replace if we want different uniqe name

                    using (FileStream fs = System.IO.File.Create(filepath))
                {
                    files.CopyTo(fs);
                    fs.Flush();
                }
            }
        }
        return View();
       }

        public IActionResult Download()
        {
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(this.Environment.WebRootPath, "Images/"));

            //Copy File names to Model collection.
            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }

            return View(files);
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Images/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
    }

}



//reference Link:https://tutexchange.com/file-upload-in-asp-net-core-mvc-storing-in-folder/

//https://tutexchange.com/uploading-multiple-files-in-asp-net-core-mvc-storing-in-folder/

// reference for Download iamge : https://www.aspsnippets.com/Articles/ASPNet-Core-MVC-Download-Files-from-Folder-Directory.aspx