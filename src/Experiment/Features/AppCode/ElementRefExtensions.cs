using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Experiment.Features.AppCode
{
    public static class ElementRefExtensions
    {
        public static async Task FocusAsync(this ElementReference elementRef, IJSRuntime runtime) => await JSInteropHelpers.FocusAsync(runtime, elementRef);
    }
}
