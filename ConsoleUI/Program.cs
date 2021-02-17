using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            GetCars(carManager);

            Console.WriteLine("-----------Get By ID------------");
            foreach (var car in carManager.GetById(2))
            {
                Console.WriteLine("Car Id = {0} Brand Id = {1} Color Id = {2} Daily Price = {3} Model Year = {4} Description = {5}"
                   , car.CarId, car.BrandId, car.ColorId, car.DailyPrice, car.ModelYear, car.Description);
            }

            Console.WriteLine("----------Add------------");
            Car car1 = new Car { CarId = 5, BrandId = 4, ColorId = 3, DailyPrice = 800, Description = "Deri Koltuk", ModelYear = 2011 };
            carManager.Add(car1);
            GetCars(carManager);

            Console.WriteLine("----------Update----------");
            car1.DailyPrice = 1000;
            carManager.Update(car1);
            GetCars(carManager);

            Console.WriteLine("------Delete------");
            carManager.Delete(car1);
            GetCars(carManager);
        }

        private static void GetCars(CarManager carManager)
        {
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Car Id = {0} Brand Id = {1} Color Id = {2} Daily Price = {3} Model Year = {4} Description = {5}"
                    , car.CarId, car.BrandId, car.ColorId, car.DailyPrice, car.ModelYear, car.Description);
            }
        }

    }
}
