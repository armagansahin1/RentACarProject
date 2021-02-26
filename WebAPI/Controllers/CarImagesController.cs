using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Models;
using System.IO;
using Core.Utilities;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        ICarImageService _carImageService;
        private IWebHostEnvironment _webHostEnvironment;
      

        public CarImagesController(ICarImageService carImageService,IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] ImageUpload objectFile)
        {
            if (objectFile.Image.Length > 0)
            {

                string path = _webHostEnvironment.WebRootPath + "\\CarImages\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }



                string guidImageName = GuidCreator.Create(objectFile.Image.FileName);







                using (FileStream fileStream = System.IO.File.Create(path + guidImageName))
                { 
                        
                        var result = _carImageService.Add(new CarImage{CarId = objectFile.CarId,Date = DateTime.Now,ImagePath = path+guidImageName});
                    if (result.Success)
                    {
                            objectFile.Image.CopyTo(fileStream);
                            fileStream.Flush();
                            return Ok(result);
                    }

                    return BadRequest(result);
                }
                
            }


            
            
            return BadRequest();
        }


        [HttpPost("delete")]
        public IActionResult Delete(int carImageId)
        {
            var result = _carImageService.Delete(carImageId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        

    }
}
