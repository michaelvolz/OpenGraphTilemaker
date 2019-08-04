using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features.Form
{
    public class ValidationErrorModel<TItem> : ComponentBase
    {
#nullable disable
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Property { get; set; }
        [Parameter] protected TItem Subject { get; set; }
#nullable enable

        protected bool HasValidationFailures => ValidationFailures.Any();
        protected IList<ValidationFailure> ValidationFailures { get; set; } = new List<ValidationFailure>();

        protected override async Task OnParametersSetAsync() =>
            ValidationFailures = await ((IValidate)Subject).ValidateAsync(Subject, Property);
    }
}