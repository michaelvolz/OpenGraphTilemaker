using Common;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.CryptoWatch
{
    public class CryptoWatchCardModel : BlazorComponentStateful<CryptoWatchCardModel>
    {
#nullable disable
        [IoC] [Parameter] protected CryptoWatchCardData Data { get; set; }
#nullable enable
    }
}
