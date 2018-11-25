using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public partial class NavMenuState : State<NavMenuState>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NavMenuState" /> class.
        ///     Required Parameterless constructor for automatic creation of the State.
        /// </summary>
        [IoC]
        public NavMenuState() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NavMenuState" /> class.
        ///     Creates new instance based off an existing one.
        /// </summary>
        /// <remarks>Constructor used for Clone.</remarks>
        /// <param name="state">The item we want to clone.</param>
        protected NavMenuState(NavMenuState state) {
            IsServerMode = state.IsServerMode;
        }

        /// <summary>
        ///     Clone the existing object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override object Clone() => new NavMenuState(this);

        /// <summary>
        ///     Set the Initial State.
        /// </summary>
        protected override void Initialize() => IsServerMode = false;
    }
}
