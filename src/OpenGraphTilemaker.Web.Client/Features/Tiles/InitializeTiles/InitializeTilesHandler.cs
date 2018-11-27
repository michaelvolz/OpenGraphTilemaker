using System;
using System.Collections.Generic;
using System.Linq;
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

                TilesState.OriginalTiles = new List<OpenGraphMetadata>();
            }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<TilesState> Handle(InitializeTilesRequest req, CancellationToken token) {
                var entries = await _pocket.GetEntriesAsync(_pocketOptions);

                var tasks = new List<Task<OpenGraphMetadata>>();

                foreach (var entry in entries) {
                    tasks.Add(_tileMakerClient.OpenGraphMetadataAsync(new Uri(entry.Link), entry));
                }

                var taskResults = await Task.WhenAll(tasks);

                foreach (var entry in taskResults) {
                    if (entry == null || !entry.IsValid) continue;
                    TilesState.OriginalTiles.Add(entry);
                }

                TilesState.OriginalTiles = TilesState.OriginalTiles.Distinct().ToList();
                TilesState.CurrentTiles = TilesState.OriginalTiles;

                return TilesState;
            }
        }
    }
}
