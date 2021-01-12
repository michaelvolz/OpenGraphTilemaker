using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.FormControl
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Utility class")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "TODO")]
    public class MyValidationSummaryModel<TItem> : ComponentBase
    {
        [Parameter] public TItem Subject { get; [UsedImplicitly] set; } = default!;
        [Parameter] public string Class { get; [UsedImplicitly] set; } = string.Empty;

        protected IList<ValidationFailure> ValidationFailures { get; set; } = new List<ValidationFailure>();
        protected bool HasValidationFailures => ValidationFailures.Any();

        protected override async Task OnParametersSetAsync() => ValidationFailures = await ((IValidate)Subject!).ValidateAsync(Subject);
    }
}
