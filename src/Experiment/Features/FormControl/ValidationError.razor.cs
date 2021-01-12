using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.FormControl
{
    public class ValidationErrorModel<TItem> : ComponentBase
    {
        [Parameter] public string Class { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public string Property { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public TItem Subject { get; [UsedImplicitly] set; } = default!;

        protected bool HasValidationFailures => ValidationFailures.Any();
        protected IList<ValidationFailure> ValidationFailures { get; private set; } = new List<ValidationFailure>();

        protected override async Task OnParametersSetAsync() =>
            ValidationFailures = await ((IValidate)Subject!).ValidateAsync(Subject, Property);
    }
}
