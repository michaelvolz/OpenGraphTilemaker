using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class Tile
    {
        [Parameter]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public OpenGraphMetadata? OpenGraphMetadata { get; [UsedImplicitly] set; }
    }
}