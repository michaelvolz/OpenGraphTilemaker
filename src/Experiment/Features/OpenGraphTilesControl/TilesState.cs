﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTilesControl
{
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Needed for Blazor.State")]
    public partial class TilesState
    {
        public IList<OpenGraphMetadata> FilteredAndSortedTiles { get; private set; } = new List<OpenGraphMetadata>();

        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Unnecessary")]
        public IList<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        public Dictionary<string, int>? TagCloud { get; private set; }

        public string SortProperty { get; private set; } = string.Empty;
        public SortOrder SortOrder { get; private set; } = SortOrder.Undefined;

        public string SearchText { get; private set; } = string.Empty;
    }
}
