using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using MediatR;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    [IoC]
    public class FetchTilesHandler : IRequestHandler<FetchTilesRequest, FetchTilesResponse>
    {
        private readonly IPocket _pocket;
        private readonly IPocketOptions _pocketOptions;
        private readonly ITileMakerClient _tileMakerClient;

        public FetchTilesHandler(IPocket pocket, ITileMakerClient client, IOptions<PocketOptions> options) {
            _pocket = pocket;
            _tileMakerClient = client;
            _pocketOptions = options.Value;
        }

        public async Task<FetchTilesResponse> Handle(FetchTilesRequest req, CancellationToken token) {
            var entries = await _pocket.GetEntriesAsync(_pocketOptions);

            var tasks = new List<Task<OpenGraphMetadata>>();

            foreach (var entry in entries) tasks.Add(_tileMakerClient.OpenGraphMetadataAsync(new Uri(entry.Link), entry));

            var taskResults = await Task.WhenAll(tasks);
            var originalTiles = new List<OpenGraphMetadata>();

            foreach (var entry in taskResults) {
                if (entry == null || !entry.IsValid) continue;
                originalTiles.Add(entry);
            }

            originalTiles = originalTiles.Distinct().ToList();

            return new FetchTilesResponse { OriginalTiles = originalTiles };
        }
    }
}
