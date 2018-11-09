using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TilesModel : BlazorComponent
    {
        [Inject] private AppState AppState { get; set; }
        [Inject] private IMemoryCache MemoryCache { get; set; }
        [Inject] private ITileMakerClient TileMakerClient { get; set; }

        [Parameter] protected List<OpenGraphMetadata> MyTiles { get; set; }

        protected override async Task OnInitAsync()
        {
            AppState.OnSort += StateHasChanged;

            if (AppState.Urls.Any())
                AppState.Urls = new List<GetPocketEntry>();

            if (AppState.Tiles.Any())
                AppState.Tiles = new List<OpenGraphMetadata>();

            var pocket = new GetPocket(MemoryCache);

            AppState.Urls = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/"),
                TimeSpan.FromSeconds(15));

            foreach (var pocketEntry in AppState.Urls)
            {
                var openGraphMetadata = await TileMakerClient.OpenGraphMetadataAsync(new Uri(pocketEntry.Link));

                AppState.Tiles.Add(openGraphMetadata);
            }

            AppState.Sort();
        }
    }
}