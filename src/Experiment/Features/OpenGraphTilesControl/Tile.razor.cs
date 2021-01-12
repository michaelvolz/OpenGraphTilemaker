using System.Diagnostics.CodeAnalysis;
using Domain.OpenGraphTilemaker.OpenGraph;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class Tile
    {
        [Parameter]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Blazor rule")]
        public OpenGraphMetadata? OpenGraphMetadata { get; [UsedImplicitly] set; }
    }
}
