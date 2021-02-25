using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {

            IResult result = BusinessRules.Run(CheckRentability(rental.CarId));
            if (result != null)
            {
                return result;
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

        public IResult CheckRentability(int carId)
        {
            if (_rentalDal.GetAll().Exists(r => r.CarId == carId && r.ReturnDate == null))
            {
                return new ErrorResult(Messages.CantRentMessage);
            }

            return new SuccessResult();
        }
    }
}
