using Microsoft.AspNetCore.Blazor;

namespace OpenGraphTilemaker.Web.Client.Features.Tabs
{
    public interface ITab
    {
        RenderFragment ChildContent { get; }
    }
}
