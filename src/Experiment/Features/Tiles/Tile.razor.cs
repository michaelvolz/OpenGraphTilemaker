using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public class TileModel : ComponentBase
    {
        [Parameter] public OpenGraphMetadata? OpenGraphMetadata { get; [UsedImplicitly] set; }
    }
}