﻿using FluentValidation;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {
            RuleFor(p => p.FirstName).NotNull().NotEmpty().NotEqual("John");
            RuleFor(p => p.LastName).NotNull().NotEmpty();
            RuleFor(p => p.Age).Must(age => age > 18).WithMessage("Your age has to be at least '18'.");
        }
    }
}
