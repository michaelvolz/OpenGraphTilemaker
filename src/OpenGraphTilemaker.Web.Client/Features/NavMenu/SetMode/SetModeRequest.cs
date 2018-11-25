using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public class SetModeRequest : IRequest<NavMenuState>
    {
        public bool IsServerMode { get; set; }
    }
}
