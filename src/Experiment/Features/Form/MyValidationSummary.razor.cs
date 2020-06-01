using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features.Form
{
    public class MyValidationSummaryModel<TItem> : ComponentBase
    {
#nullable disable
        [Parameter] public TItem Subject { get; set; }
#nullable enable

        [Parameter] public string Class { get; set; } = string.Empty;

        protected IList<ValidationFailure> ValidationFailures { get; set; } = new List<ValidationFailure>();
        protected bool HasValidationFailures => ValidationFailures.Any();

        protected override async Task OnParametersSetAsync() => ValidationFailures = await ((IValidate)Subject).ValidateAsync(Subject);
    }
}