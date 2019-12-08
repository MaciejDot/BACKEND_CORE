using BackendCore.ModelsValidators.Account;
using ServiceStack.FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Models.Account
{
    [Validator(typeof(RegisterUserValidator))]
    public class RegisterUserModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
