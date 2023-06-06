using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class RegisterRequestValidatorDTO : AbstractValidator<RegisterRequestDTO>
    {
        public RegisterRequestValidatorDTO()
        {
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Phone is required!");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password is required!");

        }
    }
}
