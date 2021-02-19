using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car
            {
                BrandId = 1,
                ColorId=5,
                DailyPrice=650,
                Description="Uygun",
                ModelYear=2021,
            };

            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(car1);
            foreach (var car in carManager.GetCarDetails())
            {
                Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
                    car.BrandName,car.ColorName,car.ModelYear,car.DailyPrice); 
            }
            Console.WriteLine("---------Car Details From Brand Id----------------");
            foreach (var car in carManager.GetCarDetailsByBrandId(1))
            {
                Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
                    car.BrandName, car.ColorName, car.ModelYear, car.DailyPrice);
            }
            Console.WriteLine("---------Car Details From Color Id----------------");
            foreach (var car in carManager.GetCarDetailsByColorId(1))
            {
                Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
                    car.BrandName, car.ColorName, car.ModelYear, car.DailyPrice);
            }


        }
    }
}
