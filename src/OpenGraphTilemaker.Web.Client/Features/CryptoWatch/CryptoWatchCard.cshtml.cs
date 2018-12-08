using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client.Features.CryptoWatch
{
    public class CryptoWatchCardModel : BlazorComponentStateful<CryptoWatchCardModel>
    {
        [Parameter] private protected CryptoWatchCardData Data { get; set; }
    }
}
