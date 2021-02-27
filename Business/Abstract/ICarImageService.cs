using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile imageFile,CarImage carImage);
        IResult Update(CarImage carImage);
        IResult Delete(int carImageId);
       
        IDataResult<List<CarImage>> GetAll();
       

    }
}
