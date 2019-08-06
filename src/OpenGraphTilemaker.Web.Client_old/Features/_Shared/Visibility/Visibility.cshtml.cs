using Microsoft.AspNetCore.Components;

// ReSharper disable CheckNamespace

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class VisibilityModel : ComponentBase
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool Show { get; set; }
    }
}
