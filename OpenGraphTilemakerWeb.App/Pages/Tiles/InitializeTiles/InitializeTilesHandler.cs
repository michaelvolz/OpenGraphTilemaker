using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class InitializeTilesHandler : RequestHandler<InitializeTilesRequest, TilesState>
        {
            // TODO: put into ApplicationOptions!
            private readonly TimeSpan _cachingTimeSpan = TimeSpan.FromSeconds(15);

            private readonly IMemoryCache _memoryCache;

            // TODO: put into ApplicationOptions!
            private readonly Uri _pocketUri = new Uri("https://getpocket.com/users/Flynn0r/feed/");

            private readonly ITileMakerClient _tileMakerClient;

            public InitializeTilesHandler(IMemoryCache cache, ITileMakerClient client, IStore store) : base(store)
            {
                _memoryCache = cache;
                _tileMakerClient = client;
            }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<TilesState> Handle(InitializeTilesRequest req, CancellationToken token)
            {
                var pocket = new GetPocket(_memoryCache);
                var entries = await pocket.GetEntriesAsync(_pocketUri, _cachingTimeSpan);

                TilesState.Tiles.Clear();
                foreach (var entry in entries)
                {
                    var openGraphMetadata = await _tileMakerClient.OpenGraphMetadataAsync(new Uri(entry.Link));
                    if (openGraphMetadata == null) continue;

                    openGraphMetadata.SourcePublishTime = entry.PubDate;
                    TilesState.Tiles.Add(openGraphMetadata);
                }

                return TilesState;
            }
        }
    }
}