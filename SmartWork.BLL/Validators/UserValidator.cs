﻿using FluentValidation;
using SmartWork.Core.Entities;

namespace EducationPortal.BLL.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please specify a first name");
            RuleFor(x => x.SecondName).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please specify a second name");
            RuleFor(x => x.Patronymic).NotEmpty().MaximumLength(128).Matches(@"^\D+$").WithMessage("Please specify a patronymic");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
