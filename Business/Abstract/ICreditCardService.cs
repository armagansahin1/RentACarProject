using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult VerifyPayment(CreditCard creditCard);
        IResult Add(CreditCard creditCard);
        IResult Update(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<List<CreditCard>> GetAll();
        IDataResult<CreditCard> GetById(int creditCardId);
        IDataResult<CreditCard> GetByCardNumber(string cardNumber);
        IDataResult<List<CreditCard>> GetCustomerCards(int customerId);
    }
}
