using System;
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
        [Parameter] protected string Class { get; set; }

        protected bool HasValidationFailures => ValidationFailures.Any();

        [Parameter] protected Func<TItem, object> Property { get; set; }
        [Parameter] protected TItem Subject { get; set; }

        protected IList<ValidationFailure> ValidationFailures { get; set; }

        protected override async Task OnParametersSetAsync() =>
            ValidationFailures = await ((IValidate)Subject).ValidateAsync(Subject, Property.Invoke(Subject).ToString());
    }
}
