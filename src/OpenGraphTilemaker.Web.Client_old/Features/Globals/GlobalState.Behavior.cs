using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.Globals
{
    public partial class GlobalState : State<GlobalState>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalState" /> class.
        ///     Required Parameterless constructor for automatic creation of the State.
        /// </summary>
        [IoC]
        public GlobalState() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GlobalState" /> class.
        ///     Creates new instance based off an existing one.
        /// </summary>
        /// <remarks>Constructor used for Clone.</remarks>
        /// <param name="state">The item we want to clone.</param>
        protected GlobalState(GlobalState state) {
            ThemeColor1 = state.ThemeColor1;
        }

        /// <summary>
        ///     Clone the existing object.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public override object Clone() => new GlobalState(this);

        /// <summary>
        ///     Set the Initial State.
        /// </summary>
        protected override void Initialize() {
            ThemeColor1 = "orange";
            ThemeColor2 = "lightgreen";
            ThemeColor3 = "yellow";
        }
    }
}
