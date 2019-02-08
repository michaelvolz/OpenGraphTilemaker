using Microsoft.AspNetCore.Components;

namespace OpenGraphTilemaker.Web.Client.Features.Tabs
{
    public interface ITab
    {
        RenderFragment ChildContent { get; set; }
    }
}
