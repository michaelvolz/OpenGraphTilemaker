using Microsoft.AspNetCore.Components;

namespace Experiment.Features.App.Visibility
{
    public class VisibilityModel : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool Show { get; set; }
    }
}
