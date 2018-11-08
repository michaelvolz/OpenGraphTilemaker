using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TilesModel : BlazorComponent
    {
        protected List<PocketEntry> Urls { get; private set; }

        protected override async Task OnInitAsync()
        {
            var pocket = new Pocket();

            Urls = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/all"));
        }
    }
}