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

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckNumberOfPicture(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(int carImageId)
        {
            CarImage carImage = _carImageDal.Get(ci => ci.CarImageId == carImageId);
            File.Delete(@carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
           return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult Update(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckNumberOfPicture(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.NumberOfPictureError);
            }

            return new SuccessResult();
        }
    }
}
