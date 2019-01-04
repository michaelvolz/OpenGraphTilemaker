using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client.Features._Shared.Visibility
{
    public class VisibilityModel : BlazorComponent
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool Show { get; set; }
    }
}
