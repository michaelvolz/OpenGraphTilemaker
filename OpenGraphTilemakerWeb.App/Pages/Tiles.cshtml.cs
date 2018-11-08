using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TilesModel : BlazorComponent
    {
        public List<PocketEntry> Urls { get; set; }
        
        protected override async Task OnInitAsync()
        {
            var pocket = new Pocket();
            
            Urls = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/all"));
        }
    }
}
