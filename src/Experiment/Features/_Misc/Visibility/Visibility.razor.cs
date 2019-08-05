using Microsoft.AspNetCore.Components;

// ReSharper disable CheckNamespace

namespace Experiment.Features._Misc.Visibility
{
    public class VisibilityModel : ComponentBase
    {
#nullable disable
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool Show { get; set; }
#nullable enable
    }
}
