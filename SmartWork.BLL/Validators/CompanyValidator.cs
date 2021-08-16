using FluentValidation;
using SmartWork.Core.Entities;

namespace SmartWork.BLL.Validators
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please, specify a company name");
            RuleFor(x => x.CompanyAddress).NotEmpty().MaximumLength(128).WithMessage("Please, specify a company address");
            RuleFor(x => x.CompanyPhoneNumber).NotEmpty().Matches(@"^0[0-9]\d{2}-\d{3}-\d{4}$").WithMessage("Please, specify a company phone number");
            RuleFor(x => x.CompanyDescription).MaximumLength(128);
            RuleFor(x => x.PhotoFileName).NotEmpty().MaximumLength(128).WithMessage("Please, specify a photo file name");  
        }
    }
}
