using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Validators;
using Core.Entities.Concrete;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).Must(ValidMail);
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
          
        }

        private bool ValidMail(string arg)
        {
            return arg.Contains("@");
        }
    }
}
