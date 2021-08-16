using FluentValidation;
using SmartWork.Core.Entities;
using System;

namespace SmartWork.BLL.Validators
{
    public class SubscribeValidator : AbstractValidator<Subscribe>
    {
        public SubscribeValidator()
        {
            RuleFor(x => x.SubscribeName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please, specify a subscribe name");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Enter the user ID for this subscribe");
            RuleFor(x => x.SubscribeDetailId).NotEmpty().WithMessage("Enter the subscribe detail ID for this subscribe");
            RuleFor(x => x.SubscribeDescription).MaximumLength(128);
            RuleFor(x => x.StartSubscribe).NotEmpty().Must(date => date != default(DateTime)).GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("Enter valid start subscribe date");
            RuleFor(x => x.EndSubscribe).NotEmpty().Must(date => date != default(DateTime)).GreaterThan(x => x.StartSubscribe)
               .WithMessage("Enter valid end subscribe date");
        }
    }
}
