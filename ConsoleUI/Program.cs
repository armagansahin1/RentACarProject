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
            //Car car1 = new Car
            //{
            //    BrandId = 1,
            //    ColorId=5,
            //    DailyPrice=0,

            //    ModelYear=2021,
            //};

            //CarManager carManager = new CarManager(new EfCarDal());
            //Console.WriteLine(carManager.Add(car1).Message);


            //Console.WriteLine("***********************");
            //var result = carManager.GetCarDetails();

            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
            //        car.BrandName,car.ColorName,car.ModelYear,car.DailyPrice); 
            //}

            //Console.WriteLine("---------Car Details From Brand Id----------------");
            //result = carManager.GetCarDetailsByBrandId(1);
            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
            //        car.BrandName, car.ColorName, car.ModelYear, car.DailyPrice);
            //}
            //Console.WriteLine("---------Car Details From Color Id----------------");
            //result = carManager.GetCarDetailsByColorId(1);
            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(" Car Name = {0} - Color = {1} - Model Year = {2} - Daily Price = {3}",
            //        car.BrandName, car.ColorName, car.ModelYear, car.DailyPrice);
            //}
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.GetRentedCarDetail();
            foreach (var car in result.Data)
            {
                Console.WriteLine(car.BrandName);
            }

            Rental rental = new Rental();
            rental.CarId = 2004;
            rental.CustomerId = 2;
            rental.RentDate = new DateTime(2021, 2, 19);
            var result3=rentalManager.Add(rental);
            Console.WriteLine(result3.Message);

        }
    }
}
