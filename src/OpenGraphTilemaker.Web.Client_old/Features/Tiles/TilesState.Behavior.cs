﻿using System.Collections.Generic;
using BlazorState;
using Common;
using Common.TagCloud;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState : State<TilesState>
    {
        [IoC]
        public TilesState() { }

        protected TilesState(TilesState state) {
            CurrentTiles = state.CurrentTiles;
            TagCloud = state.TagCloud;

            SortOrder = state.SortOrder;
            SortProperty = state.SortProperty;

            SearchText = state.SearchText;
            LastSearchText = state.LastSearchText;
        }

        public override object Clone() => new TilesState(this);

        protected override void Initialize() {
            CurrentTiles = new List<OpenGraphMetadata>();
            TagCloud = default;

            SortOrder = SortOrder.Descending;
            SortProperty = nameof(OpenGraphMetadata.BookmarkTime);

            SearchText = string.Empty;
            LastSearchText = string.Empty;
        }
    }
}