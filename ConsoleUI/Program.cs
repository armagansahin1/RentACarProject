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
            CarManager carManager = new CarManager(new EfCarDal());

            Car car1 = new Car {BrandId=3,ColorId=2,DailyPrice=350,ModelYear=2009,Description="CLK" };

            carManager.Add(car1);
            
          
        }
    }
}
