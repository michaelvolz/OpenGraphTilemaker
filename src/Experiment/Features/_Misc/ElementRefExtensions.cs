using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Experiment.Features
{
    public static class ElementRefExtensions
    {
        public static async Task FocusAsync(this ElementRef elementRef, IComponentContext componentContext, IJSRuntime jsRuntime) => await JSInteropHelpers.FocusAsync(componentContext, jsRuntime, elementRef);
    }
}
