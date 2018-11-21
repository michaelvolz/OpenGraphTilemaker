using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Blazor.Components;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class ValidationErrorModel<TItem> : BlazorComponent
    {
        [Parameter] protected TItem Subject { get; set; }
        [Parameter] protected string Property { get; set; }

        protected IList<ValidationFailure> ValidationFailures { get; set; }
        protected bool HasValidationFailures => ValidationFailures.Any();

        protected override async Task OnParametersSetAsync() {
            ValidationFailures = await ((IValidate)Subject).ValidateAsync(Subject, Property);
        }
    }
}
