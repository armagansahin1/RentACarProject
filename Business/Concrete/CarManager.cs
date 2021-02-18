using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carManager)
        {
            _carDal = carManager;
        }

        public void Add(Car car)
        {
            if (car.DailyPrice>0)
            {
                _carDal.Add(car);
            }
            else
            {
                Console.WriteLine("Daily Price must be higher than {0}",car.DailyPrice);
            }
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

       

        public void Update(Car car)
        {
            _carDal.Update(car);
        }
        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }
        public List<Car> GetCarsByColorId(int colorId)
        {
             return _carDal.GetAll(c => c.BrandId == colorId);
        }

        public List<Car> GetById(int carId)
        {
             return _carDal.GetAll(c => c.CarId == carId);
        }

       
    }
}
