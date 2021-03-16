using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Business.Constant;
using Core.Utilities.Business;
using Core.Utilities.FileOperations;
using Microsoft.AspNetCore.Http;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        [CacheRemoveAspect("ICarImageService.Get")]
        //[SecuredOperation("admin")]
        public IResult Add(IFormFile imageFile,CarImage carImage)
        {
            var defaultImageFile = _carImageDal.Get(ci =>ci.CarId==carImage.CarId && ci.ImagePath == DefaultImageFile.FilePath + DefaultImageFile.FileName);
            DeleteDefaultFile(defaultImageFile);
            carImage.Date=DateTime.Now;
            carImage.ImagePath = FileOperations.Add(imageFile,DefaultImageFile.FilePath,DefaultImageFile.FileName);
            var result = BusinessRules.Run(CheckNumberOfImage(carImage.CarId),CheckValidFileType(carImage.ImagePath));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }
        [CacheRemoveAspect("ICarImageService.Get")]
        //[SecuredOperation("admin")]
        public IResult Delete(CarImage carImage)
        {
            var imageToDelete = _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId);
            var carImagesCount = _carImageDal.GetAll(ci => ci.CarId == imageToDelete.CarId).Count;
            AddDefaultFile(carImagesCount,imageToDelete.CarId);
            FileOperations.DeleteFile(@imageToDelete.ImagePath,CheckDefaultFile(imageToDelete.ImagePath));
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }
        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
           return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(b => b.CarId == carId));
        }
        [CacheRemoveAspect("ICarImageService.Get")]
        //[SecuredOperation("admin")]
        public IResult Update(IFormFile imageFile,CarImage carImage)
        {
            var deleteToImageFile = _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId);
            FileOperations.DeleteFile(deleteToImageFile.ImagePath,CheckDefaultFile(deleteToImageFile.ImagePath));
            carImage.Date=DateTime.Now;
            carImage.CarId = deleteToImageFile.CarId;
            carImage.ImagePath = FileOperations.Add(imageFile,DefaultImageFile.FilePath,DefaultImageFile.FileName);
            var result = BusinessRules.Run(CheckNumberOfImage(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

       
        
        
        
        
        
        private IResult CheckNumberOfImage(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.NumberOfPictureError);
            }

            return new SuccessResult();
        }

        private IResult CheckValidFileType(string imagePath)
        {
            string [] supportedFileTypes={".jpg",".jpeg",".png"};
            int startValue = imagePath.LastIndexOf(".");
            string fileType = imagePath.Substring(startValue).ToLower();
            for (int i = 0; i < supportedFileTypes.Length; i++)
            {
                if (fileType.ToLower() == supportedFileTypes[i])
                {
                    return new SuccessResult();
                }
            }
            
            return new ErrorResult(Messages.InvalidFileType);

        }

        private IResult CheckDefaultFile(string imagePath)
        {
            if (imagePath==(DefaultImageFile.FilePath+DefaultImageFile.FileName))
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private void DeleteDefaultFile(CarImage carImage)
        {
            if (carImage != null)
            {
                _carImageDal.Delete(carImage);
            }
        }

        private void AddDefaultFile(int carImagesCount,int carId)
        {
            
            if (carImagesCount == 0)
            {
                IFormFile formFile = null;
                Add(formFile, new CarImage { CarId = carId });
                 
                
            }
        }
    }
}
