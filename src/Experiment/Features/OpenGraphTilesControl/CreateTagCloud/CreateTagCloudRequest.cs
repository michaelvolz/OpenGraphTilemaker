﻿using System.Collections.Generic;
using BlazorState;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class TilesState
    {
        public class CreateTagCloudRequest : IAction
        {
            public IList<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();
        }
    }
}
