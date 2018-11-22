using System.Collections.Generic;
using BlazorState;
using Common;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState : State<TilesState>
    {
        [IoC]
        public TilesState() { }

        protected TilesState(TilesState state) {
            Tiles = state.Tiles;
            SortOrder = state.SortOrder;
            SortProperty = state.SortProperty;
        }

        public override object Clone() => new TilesState(this);

        protected override void Initialize() {
            SortProperty = nameof(OpenGraphMetadata.BookmarkTime);
            SortOrder = SortOrder.Descending;
            Tiles = new List<OpenGraphMetadata>();
        }
    }
}
