﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.FormControl
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global", Justification = "Utility class")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "TODO")]
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
