﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class TilesState
    {
        public List<OpenGraphMetadata> CurrentTiles { get; private set; } = new List<OpenGraphMetadata>();
        public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        //public IOrderedEnumerable<KeyValuePair<string, int>>? TagCloud { get; private set; }
        public Dictionary<string, int>? TagCloud { get; private set; }

        public string SortProperty { get; private set; } = string.Empty;
        public SortOrder SortOrder { get; private set; } = SortOrder.Undefined;

        public string SearchText { get; private set; } = string.Empty;
        public string LastSearchText { get; private set; } = string.Empty;
    }
}