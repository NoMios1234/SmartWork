using FluentValidation;
using SmartWork.Core.Entities;

namespace SmartWork.BLL.Validators
{
    public class SubscribeDetailsValidator : AbstractValidator<SubscribeDetail>
    {
        public SubscribeDetailsValidator()
        {
            RuleFor(x => x.SubscribeName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please, specify a subscribe name");
            RuleFor(x => x.SubscribePrice).NotEmpty().WithMessage("Please, specify a price for subscribe");
            RuleFor(x => x.SubscribeDescription).MaximumLength(128);
        }
    }
}
