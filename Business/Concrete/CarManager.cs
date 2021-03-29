using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        private ICarImageService _carImageService;

        public CarManager(ICarDal carManager,ICarImageService carImageService)
        {
            _carImageService = carImageService;
            _carDal = carManager;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            
                _carDal.Add(car);
            IFormFile formFile = null;
            _carImageService.Add(formFile, new CarImage { CarId = car.CarId });
            return new SuccessResult(Messages.AddMessage);
            
        }
        [SecuredOperation("admin")]
        public IResult Delete(Car car)
        {
            _carImageService.DeleteAllCarImage(car.CarId);
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll()); 
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.UpdateMessage);
        }

        
        public IDataResult<Car> GetById(int carId)
        {
             return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

  

        public IDataResult<CarDetailDto> GetCarDetailsByCarId(int carId)
        {
            var car = _carDal.GetCarDetails(c => c.CarId == carId);
            return new SuccessDataResult<CarDetailDto>(car.Find(c=>c.CarId==carId));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByBrandIdColorId(int brandId, int colorId)
        {
            var data = _carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId);


            return new SuccessDataResult<List<CarDetailDto>>(data);
        }
    }
}
