using FluentValidation;

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator() {
            RuleFor(p => p.FirstName).NotNull().NotEmpty().NotEqual("John");
            RuleFor(p => p.LastName).NotEmpty();
        }
    }
}
