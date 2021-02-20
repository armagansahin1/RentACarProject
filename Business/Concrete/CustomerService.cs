using Business.Abstract;
using Business.Constant;
using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        ICustomerService _customerService;

        public CustomerService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IResult Add(Customer customer)
        {
            _customerService.Add(customer);
            return new SuccessResult(Messages.AddMessage);
        }

        public IResult Delete(Customer customer)
        {
            _customerService.Delete(customer);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>();
        }

        public IResult Update(Customer customer)
        {
            _customerService.Update(customer);
            return new SuccessResult(Messages.UpdateMessage);
        }
    }
}
