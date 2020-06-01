using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Extensions;
using MediatR;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    [IoC]
    public class FetchTilesHandler : ActionHandler<TilesState.FetchTilesRequest>
    {
        private readonly IPocket _pocket;
        private readonly IPocketOptions _pocketOptions;
        private readonly ITileMakerClient _tileMakerClient;

        public FetchTilesHandler(IStore store, IPocket pocket, ITileMakerClient client, IOptions<PocketOptions> options) : base(store) {
            _pocket = pocket;
            _tileMakerClient = client;
            _pocketOptions = options.Value;
        }

        public TilesState TilesState => Store.GetState<TilesState>();

        public override async Task<Unit> Handle(TilesState.FetchTilesRequest req, CancellationToken token) {
            var entries = await _pocket.GetEntriesAsync(_pocketOptions);
            var tasks = new List<Task<OpenGraphMetadata>>();

            foreach (var entry in entries)
                tasks.Add(_tileMakerClient.OpenGraphMetadataAsync(new Uri(entry.Link), entry)
                    .TimeoutAfter(_pocketOptions.TimeOutTimeSpan));

            var taskResults = await Task.WhenAll(tasks);

            TilesState.OriginalTiles = taskResults.Where(entry => entry != null && entry.IsValid).Distinct().ToList();

            return await Unit.Task;
        }
    }
}
