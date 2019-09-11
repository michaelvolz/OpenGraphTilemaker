﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Experiment.Features.App
{
    public static class ElementRefExtensions
    {
        public static async Task FocusAsync(this ElementReference elementRef, IJSRuntime jsRuntime) => await JSInteropHelpers.FocusAsync(jsRuntime, elementRef);
    }
}