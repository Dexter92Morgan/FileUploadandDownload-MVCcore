using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FileUploadandDownloadMVCcore.Models
{
    public class AttachmentViewModel
    {
        public string FileName { set; get; }
        public string Description { set; get; }
        public IFormFile attachment { set; get; }
        public List<Attachment> attachments { get; set; }
    }
}
