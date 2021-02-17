using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car> {
                new Car { CarId=1,BrandId=1,ColorId=2,DailyPrice=250,Description="Günlük Kiralama İçin Uygun",ModelYear=2010},
                new Car { CarId=2,BrandId=3,ColorId=1,DailyPrice=1250,Description="Başkası Tarafından Kiralanmış",ModelYear=2012},
                new Car { CarId=3,BrandId=2,ColorId=4,DailyPrice=350,Description="Araba Bakımda",ModelYear=2009},
                new Car { CarId=4,BrandId=1,ColorId=5,DailyPrice=3000,Description="Uygun Değil",ModelYear=2019},
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
            
        }

        public void Delete(Car car)
        {
            Car deletedCar = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            _cars.Remove(deletedCar);
            
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetById(int id)
        {
            return _cars.Where(c => c.CarId == id).ToList();
            
            
        }

        public void Update(Car car)
        {
            Car updatedCar = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            updatedCar.BrandId = car.BrandId;
            updatedCar.ColorId = car.ColorId;
            updatedCar.DailyPrice = car.DailyPrice;
            updatedCar.DailyPrice = car.DailyPrice;
            updatedCar.Description = car.Description;
        }
    }
}
