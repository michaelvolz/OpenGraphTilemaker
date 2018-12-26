using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client.Features.ToggleVisibility
{
    public class VisibilityModel : BlazorComponent
    {
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected VisibilityState State { get; set; } = VisibilityState.Undefined;
    }

    public enum VisibilityState
    {
        Undefined = 0,
        Show = 1,
        Hide = 2,
    }
}
