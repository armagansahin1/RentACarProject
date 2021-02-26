using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ImageUpload
    {
        public IFormFile Image { get; set; }
        public int CarId { get; set; }
    }
}
