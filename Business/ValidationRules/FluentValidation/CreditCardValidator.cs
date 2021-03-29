using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardValidator:AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CVV).NotEmpty();
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.ExpYear).NotEmpty();
            RuleFor(c => c.ExpMonth).NotEmpty();

            RuleFor(c => c.ExpMonth).InclusiveBetween(1, 12);
            RuleFor(c => c.ExpYear).InclusiveBetween(DateTime.Now.Year, DateTime.Now.Year + 50);
            RuleFor(c => c.CardNumber).MinimumLength(16);
            RuleFor(c => c.CardNumber).MaximumLength(16);
            RuleFor(c => c.CVV).InclusiveBetween(100, 999);
        }
       
    }
}
