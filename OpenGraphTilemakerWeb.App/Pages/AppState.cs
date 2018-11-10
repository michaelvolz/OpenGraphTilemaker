using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class AppState
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ITileMakerClient _tileMakerClient;

        public AppState(IMemoryCache memoryCache, ITileMakerClient tileMakerClient)
        {
            _memoryCache = memoryCache;
            _tileMakerClient = tileMakerClient;
        }

        private List<GetPocketEntry> Urls { get; set; } = new List<GetPocketEntry>();

        public List<OpenGraphMetadata> Tiles { get; private set; } = new List<OpenGraphMetadata>();

        public string SortProperty { get; private set; } = nameof(OpenGraphMetadata.SourcePublishTime);

        public SortOrder SortOrder { get; private set; } = SortOrder.Descending;

        public int TileCount { get; private set; }

        public event Action OnTilesChanged;

        private void TilesChanged() => OnTilesChanged?.Invoke();

        /// <summary>
        ///     Initialize
        /// </summary>
        public async Task Initialize()
        {
            var pocket = new GetPocket(_memoryCache);
            Urls = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/"),
                TimeSpan.FromSeconds(15));

            foreach (var pocketEntry in Urls)
            {
                var openGraphMetadata = await _tileMakerClient.OpenGraphMetadataAsync(new Uri(pocketEntry.Link));
                openGraphMetadata.SourcePublishTime = pocketEntry.PubDate;

                Tiles.Add(openGraphMetadata);
            }

            SortTiles();
        }

        /// <summary>
        ///     SortTiles
        /// </summary>
        /// <param name="sortProperty"></param>
        /// <param name="sortOrder"></param>
        public void SortTiles(string sortProperty = null, SortOrder sortOrder = SortOrder.Undefined)
        {
            if (sortProperty != null)
                SortProperty = sortProperty;

            if (sortOrder != SortOrder.Undefined)
                SortOrder = sortOrder;

            switch (SortProperty)
            {
                case nameof(OpenGraphMetadata.Title):
                    Tiles = SortOrder == SortOrder.Ascending
                        ? Tiles.OrderBy(f => f.Title != null).ThenBy(f => f.Title).ToList()
                        : Tiles.OrderByDescending(f => f.Title == null).ThenByDescending(f => f.Title).ToList();
                    break;
                case nameof(OpenGraphMetadata.SourcePublishTime):
                    Tiles = SortOrder == SortOrder.Ascending
                        ? Tiles.OrderBy(f => f.SourcePublishTime).ToList()
                        : Tiles.OrderByDescending(f => f.SourcePublishTime).ToList();
                    break;
            }

            TileCount = Tiles.Count;

            TilesChanged();
        }
    }
}