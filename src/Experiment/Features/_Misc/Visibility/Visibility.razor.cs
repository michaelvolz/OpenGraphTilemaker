using Microsoft.AspNetCore.Components;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features._Misc.Visibility
{
    public class VisibilityModel : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool Show { get; set; }
    }
}
