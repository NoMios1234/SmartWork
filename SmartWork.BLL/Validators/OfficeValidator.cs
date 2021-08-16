using FluentValidation;
using SmartWork.Core.Entities;

namespace SmartWork.BLL.Validators
{
    public class OfficeValidator : AbstractValidator<Office>
    {
        public OfficeValidator()
        {
            RuleFor(x => x.OfficeName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please, specify an office name");
            RuleFor(x => x.OfficeAddress).NotEmpty().MaximumLength(128).WithMessage("Please, specify an office address");
            RuleFor(x => x.OfficePhoneNumber).NotEmpty().Matches(@"^[0-9]\d{2}-\d{3}-\d{4}$").WithMessage("Please, specify an office phone number");
            RuleFor(x => x.PhotoFileName).NotEmpty().MaximumLength(128).WithMessage("Please, specify a photo file name");
            RuleFor(x => x.CompanyId).NotEmpty().GreaterThan(0).WithMessage("Enter the company ID for this office");
        }
    }
}
