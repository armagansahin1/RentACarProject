using Business.Abstract;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
   
   public class DebitCardManager:IDebitCardService
    {
        IDebitCardDal _debitCardDal;
        public DebitCardManager(IDebitCardDal debitCardDal)
        {
            _debitCardDal = debitCardDal;
        }

        public IResult VerifyPayment(DebitCard debitCard)
        {
            var checkCard =_debitCardDal.Get(d => d.CardNumber == debitCard.CardNumber);
            if(checkCard == null)
            {
                return new ErrorResult("Geçersiz Banka Kartı !!!");
            }
            if(checkCard.FirstName.ToUpper()==debitCard.FirstName.ToUpper() && checkCard.LastName.ToUpper()==debitCard.LastName.ToUpper() && 
                checkCard.Password.Trim()==debitCard.Password && checkCard.ExpYear==debitCard.ExpYear 
                && checkCard.ExpMonth == debitCard.ExpMonth && checkCard.CCV==debitCard.CCV)
            {
                return new SuccessResult("Ödeme Gerçekleştirildi !!!");
            }


            return new ErrorResult("Kart Bilgileriniz Yanlıştır !!!");
            
            
           

        }
    }
}
