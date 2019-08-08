#nullable enable
using Microsoft.AspNetCore.Components;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features._Misc.Visibility
{
    public class VisibilityModel : ComponentBase
    {
        [Parameter] protected RenderFragment? ChildContent { get; set; }
        [Parameter] protected bool Show { get; set; }
    }
}
