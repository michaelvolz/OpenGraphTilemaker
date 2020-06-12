using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.CryptoWatch
{
    public partial class CryptoWatchCard
    {
        [Parameter]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
        public CryptoWatchCardData Data { get; [UsedImplicitly] set; } = null!;
    }
}
