﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Blazor.Components;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemaker.Web.Client.Features.Form
{
    public class ValidationSummaryModel : BlazorComponent
    {
        [Parameter] protected object Subject { get; set; }

        protected IList<ValidationFailure> ValidationFailures { get; set; }
        protected bool HasValidationFailures => ValidationFailures.Any();

        protected override async Task OnParametersSetAsync() {
            ValidationFailures = await ((IValidate)Subject).ValidateAsync();
        }
    }
}
