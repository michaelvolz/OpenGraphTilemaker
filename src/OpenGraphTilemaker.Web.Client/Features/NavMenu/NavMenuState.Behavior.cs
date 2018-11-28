using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public partial class NavMenuState : State<NavMenuState>
    {
        [IoC]
        public NavMenuState() { }

        protected NavMenuState(NavMenuState state) {
            BlazorMode = state.BlazorMode;
        }

        public override object Clone() => new NavMenuState(this);

        protected override void Initialize() => BlazorMode = BlazorMode.Undefined;
    }
}
