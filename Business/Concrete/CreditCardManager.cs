using Business.Abstract;
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

        private IResult Verify(CreditCard creditCard)
        {
            var checkCard = _creditCardDal.Get(d => d.CardNumber == creditCard.CardNumber);
            
            if (checkCard == null)
            {
                return new ErrorResult("Geçersiz Banka Kartı !!!");
            }
            if (checkCard.FirstName.ToUpper() != creditCard.FirstName.ToUpper() || checkCard.LastName.ToUpper() != creditCard.LastName.ToUpper() ||
                checkCard.ExpYear != creditCard.ExpYear
                || checkCard.ExpMonth != creditCard.ExpMonth || checkCard.CCV != creditCard.CCV)
            {
                return new ErrorResult("Kart Bilgileriniz Yanlıştır !!!");
            }


            return new SuccessResult();

        }
    }
}
