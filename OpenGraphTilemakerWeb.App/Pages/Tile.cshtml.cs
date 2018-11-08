using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TileModel : BlazorComponent
    {
        [Inject] public IHttpClientFactory HttpClientFactory { get; set; }

        protected OpenGraphMetadata OpenGraphMetadata { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] private string Url { get; set; }


        protected override async Task OnInitAsync()
        {
            var tileMaker = new TileMaker();
            var httpClient = HttpClientFactory.CreateClient();
            await tileMaker.ScrapeHtmlAsync(httpClient, new Uri(Url));

            OpenGraphMetadata = tileMaker.OpenGraphMetadata;
        }
    }
}