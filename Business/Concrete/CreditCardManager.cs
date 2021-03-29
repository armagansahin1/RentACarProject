using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Results;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
   
   public class CreditCardManager:ICreditCardService
    {
        ICreditCardDal _creditCardDal;
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult VerifyPayment(CreditCard creditCard)
        {
            
            var result = BusinessRules.Run(Verify(creditCard));
            if(result != null)
            {
                return result;
            }
            return new SuccessResult("Ödeme Gerçekleştirildi");
            

        }

        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult();
        }
        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult();
        }
        public IDataResult<List<CreditCard>> GetAll()
        {
            
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }
        public IDataResult<CreditCard> GetById(int creditCardId)
        {

            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c=>c.Id==creditCardId));
        }
        

        private IResult Verify(CreditCard creditCard)
        {
            var checkCard = _creditCardDal.Get(d => d.CardNumber == creditCard.CardNumber);
            
            if (checkCard == null)
            {
                return new ErrorResult("Geçersiz Banka Kartı !!!");
            }
            if (checkCard.FirstName.ToUpper() != creditCard.FirstName.ToUpper() || checkCard.LastName.ToUpper() != creditCard.LastName.ToUpper() ||
                checkCard.ExpYear != creditCard.ExpYear
                || checkCard.ExpMonth != creditCard.ExpMonth || checkCard.CVV != creditCard.CVV)
            {
                return new ErrorResult("Kart Bilgileriniz Yanlıştır !!!");
            }


            return new SuccessResult();

        }

        public IDataResult<CreditCard> GetByCardNumber(string cardNumber)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.CardNumber == cardNumber));
        }

        public IDataResult<List<CreditCard>> GetCustomerCards(int customerId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c=>c.CustomerId==customerId));
        }
    }
}
