using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.Form
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    public class ValidationErrorModel<TItem> : ComponentBase
    {
        [Parameter] public string Class { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public string Property { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public TItem Subject { get; [UsedImplicitly] set; } = default!;

        protected bool HasValidationFailures => ValidationFailures.Any();
        protected IList<ValidationFailure> ValidationFailures { get; set; } = new List<ValidationFailure>();

        protected override async Task OnParametersSetAsync() =>
            ValidationFailures = await ((IValidate)Subject!).ValidateAsync(Subject, Property);
    }
}
