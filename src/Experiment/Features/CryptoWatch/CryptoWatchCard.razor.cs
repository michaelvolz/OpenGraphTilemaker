using System.Diagnostics.CodeAnalysis;
using Experiment.Features.App;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.CryptoWatch
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public class CryptoWatchCardModel : BlazorComponentStateful<CryptoWatchCardModel>
    {
        [Parameter] public CryptoWatchCardData Data { get; [UsedImplicitly] set; } = null!;
    }
}
