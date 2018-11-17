using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class InitializeTilesHandler : RequestHandler<InitializeTilesRequest, TilesState>
        {
            private readonly IPocket _pocket;
            private readonly IPocketOptions _pocketOptions;
            private readonly ITileMakerClient _tileMakerClient;

            public InitializeTilesHandler(IPocket pocket, ITileMakerClient client, IOptions<PocketOptions> options, IStore store) : base(store) {
                _pocket = pocket;
                _tileMakerClient = client;
                _pocketOptions = options.Value;
            }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<TilesState> Handle(InitializeTilesRequest req, CancellationToken token) {
                var entries = await _pocket.GetEntriesAsync(_pocketOptions);

                TilesState.Tiles.Clear();
                foreach (var entry in entries) {
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