using Business.Abstract;
using Business.Constant;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            var rentedCars = _rentalDal.GetAll(r => r.CarId==rental.CarId);
            if (rentedCars.Count > 0)
            {
                foreach (var car in rentedCars)
                {
                    if (car.ReturnDate==null)
                    {
                        return new ErrorResult(Messages.CantRentMessage);
                    }
                }
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentMessage);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Rental>> GetAll()
        {

            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentalDetailDto>> GetRentedCarDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentedCarDetail());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.UpdateMessage);
        }

       
    }
}
