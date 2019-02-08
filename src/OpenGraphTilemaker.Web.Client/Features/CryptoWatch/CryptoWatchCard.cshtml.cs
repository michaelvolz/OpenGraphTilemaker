using Common;
using Microsoft.AspNetCore.Components;

namespace OpenGraphTilemaker.Web.Client.Features.CryptoWatch
{
    public class CryptoWatchCardModel : BlazorComponentStateful<CryptoWatchCardModel>
    {
        [IoC] [Parameter] protected CryptoWatchCardData Data { get; set; }
    }
}
