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
using Business.BusinessAspect.Autofac;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICreditCardService _debitCardService;
        ICustomerService _customerService;
        ICarService _carService;
       
        public RentalManager(IRentalDal rentalDal, ICreditCardService debitCardService, ICustomerService customerService,ICarService carService)
        {
            _rentalDal = rentalDal;
            _debitCardService = debitCardService;
            _customerService = customerService;
            _carService = carService;
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {

            IResult result = BusinessRules.Run(CheckRentability(rental),CheckFineks(rental));
            if (result != null)
            {
                return result;
            }
            IncreaseCustomerFindeks(rental);
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentMessage);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeleteMessage);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Rental>> GetAll()
        {

            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }
        [SecuredOperation("admin,customer")]
        public IDataResult<List<RentalDetailDto>> GetRentedCarDetail()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentedCarDetail());
        }
        
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.UpdateMessage);
        }
        [ValidationAspect(typeof(RentalValidator))]
        public IResult CheckRentability(Rental rental)
        {
            bool check=true;

            var rentalList=_rentalDal.GetAll(r => r.CarId == rental.CarId);

            foreach (var data in rentalList)
            {
                if(data.RentDate<=rental.RentDate && data.ReturnDate>=rental.RentDate)
                {

                    check = false;
                    
                }
                if (data.ReturnDate >= rental.ReturnDate && data.RentDate <= rental.ReturnDate)
                {
                    check = false;
                }
                if(data.RentDate >= rental.RentDate && data.ReturnDate <= rental.ReturnDate)
                {
                    check = false;
                }
            }
            if (!check)
            {
                return new ErrorResult(Messages.CantRentMessage);
            }
            return new SuccessResult();
        }

        public IDataResult<List<RentalDetailDto>> GetInComingAppointments(int customerId)
        {
            var dateNow = DateTime.Now;
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentedCarDetail(r=>r.RentDate.Date>dateNow.Date && r.CustomerId==customerId));
        }

        public IDataResult<List<RentalDetailDto>> PastAppointments(int customerId)
        {
            var dateNow = DateTime.Now;
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentedCarDetail(r => r.RentDate.Date < dateNow.Date && r.CustomerId==customerId));
        }

        private IResult CheckFineks(Rental rental)
        {
            int customerFindeks = _customerService.GetById(rental.CustomerId).Data.FindeksPoint;
            int carFindeks = _carService.GetById(rental.CarId).Data.Findeks;
            if (customerFindeks >=carFindeks)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Bu aracı kiralamak için findeks puanınız yetersiz");
        }

        private void IncreaseCustomerFindeks(Rental rental)
        {
            var customer = _customerService.GetById(rental.CustomerId).Data;
            if(customer.FindeksPoint < 1900)
            {
                customer.FindeksPoint = customer.FindeksPoint + 100;
                _customerService.Update(customer);
            }
           
        }
    }
}
