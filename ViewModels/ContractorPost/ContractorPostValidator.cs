using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ContractorPost
{
    public class ContractorPostValidator : AbstractValidator<ContractorPostModels>
    {
        public ContractorPostValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title is required!");
            RuleFor(x => x.ProjectName).NotNull().NotEmpty().WithMessage("ProjectName is required!");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required!");
            RuleFor(x => x.StarDate).NotNull().NotEmpty();
            RuleFor(x => x.EndDate).NotNull().NotEmpty();
            RuleFor(x => x.Place).NotNull().NotEmpty();
            RuleFor(x => x.NumberPeople).NotNull().NotEmpty();

        }
    }
}
