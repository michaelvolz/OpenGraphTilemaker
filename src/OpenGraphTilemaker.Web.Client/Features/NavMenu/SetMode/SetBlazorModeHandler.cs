using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public partial class NavMenuState
    {
        [IoC]
        public class SetBlazorModeHandler : RequestHandler<SetBlazorModeRequest, NavMenuState>
        {
            public SetBlazorModeHandler(IStore store) : base(store) { }

            public NavMenuState NavMenuState => Store.GetState<NavMenuState>();

            public override Task<NavMenuState> Handle(SetBlazorModeRequest req, CancellationToken token) {
                NavMenuState.BlazorMode = req.BlazorMode;

                return Task.FromResult(NavMenuState);
            }
        }
    }
}
