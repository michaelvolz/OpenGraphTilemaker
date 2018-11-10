using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class InitializeTilesHandler : RequestHandler<InitializeTilesRequest, TilesState>
        {
            private readonly Uri _pocketUri = new Uri("https://getpocket.com/users/Flynn0r/feed/");
            private readonly TimeSpan _cachingTimeSpan = TimeSpan.FromSeconds(15);
            
            private readonly IMemoryCache _memoryCache;
            private readonly ITileMakerClient _tileMakerClient;

            public InitializeTilesHandler([NotNull] IMemoryCache memoryCache,
                [NotNull] ITileMakerClient tileMakerClient, IStore store) : base(store)
            {
                if (store == null) throw new ArgumentNullException(nameof(store));
                _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
                _tileMakerClient = tileMakerClient ?? throw new ArgumentNullException(nameof(tileMakerClient));
            }

            public TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<TilesState> Handle(InitializeTilesRequest request,
                CancellationToken token)
            {
                var pocket = new GetPocket(_memoryCache);
                var urls = await pocket.GetEntriesAsync(_pocketUri, _cachingTimeSpan);

                TilesState.Tiles.Clear();
                foreach (var pocketEntry in urls)
                {
                    var openGraphMetadata = await _tileMakerClient.OpenGraphMetadataAsync(new Uri(pocketEntry.Link));
                    openGraphMetadata.SourcePublishTime = pocketEntry.PubDate;

                    TilesState.Tiles.Add(openGraphMetadata);
                }

                return TilesState;
            }
        }
    }
}