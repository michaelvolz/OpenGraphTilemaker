using System.Collections.Generic;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class TilesState : State<TilesState>
    {
        [IoC]
        public TilesState() { }

        protected TilesState(TilesState state)
        {
            Guard.Against.Null(state, nameof(state));

            FilteredAndSortedTiles = state.FilteredAndSortedTiles;
            TagCloud = state.TagCloud;

            SortOrder = state.SortOrder;
            SortProperty = state.SortProperty;

            SearchText = state.SearchText;
        }

        public override void Initialize()
        {
            FilteredAndSortedTiles = new List<OpenGraphMetadata>();
            TagCloud = default;

            SortOrder = SortOrder.Descending;
            SortProperty = nameof(OpenGraphMetadata.BookmarkTime);

            SearchText = string.Empty;
        }
    }
}
