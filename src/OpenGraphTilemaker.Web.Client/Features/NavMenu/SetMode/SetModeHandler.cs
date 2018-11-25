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
        public class SetModeHandler : RequestHandler<SetModeRequest, NavMenuState>
        {
            public SetModeHandler(IStore store) : base(store) { }

            public NavMenuState NavMenuState => Store.GetState<NavMenuState>();

            public override Task<NavMenuState> Handle(SetModeRequest req, CancellationToken token) {
                NavMenuState.IsServerMode = req.IsServerMode;

                return Task.FromResult(NavMenuState);
            }
        }
    }
}
