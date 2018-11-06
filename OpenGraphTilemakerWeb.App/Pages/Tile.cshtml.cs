using System;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TileModel : BlazorComponent
    {
        protected OpenGraphMetadata OpenGraphMetadata { get; private set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] private string Url { get; set; }

        protected override void OnInit()
        {
            base.OnInit();

            var tileMaker = new TileMaker();
            tileMaker.ScrapeHtml(new Uri(Url));

            OpenGraphMetadata = tileMaker.OpenGraphMetadata;
        }
    }
}