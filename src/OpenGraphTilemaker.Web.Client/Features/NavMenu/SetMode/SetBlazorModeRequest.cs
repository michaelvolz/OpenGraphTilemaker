using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public class SetBlazorModeRequest : IRequest<NavMenuState>
    {
        public BlazorMode BlazorMode { get; set; }
    }
}
