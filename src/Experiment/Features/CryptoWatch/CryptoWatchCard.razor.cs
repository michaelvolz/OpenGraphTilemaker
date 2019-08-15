using Microsoft.AspNetCore.Components;

namespace Experiment.Features.CryptoWatch
{
    public class CryptoWatchCardModel : BlazorComponentStateful<CryptoWatchCardModel>
    {
#nullable disable
        [Parameter] public CryptoWatchCardData Data { get; set; }
#nullable enable
    }
}
