using BackendCore.Models.Account;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.ModelsValidators.Account
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Username).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
