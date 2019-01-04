using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

// ReSharper disable CheckNamespace

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class VisibilityModel : BlazorComponent
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected bool Show { get; set; }
    }
}
