using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.TagCloud;
using MediatR;

namespace Experiment.Features.OpenGraphTiles
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public partial class TilesState
    {
        [IoC]
        public class CreateTagCloudHandler : ActionHandler<CreateTagCloudRequest>
        {
            public CreateTagCloudHandler(IStore store) : base(store) { }

            public TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<Unit> Handle(CreateTagCloudRequest aAction, CancellationToken aCancellationToken) {
                aAction = Guard.Against.Null(() => aAction);

                var tagCloud = new TagCloud();

                foreach (var tile in aAction.OriginalTiles)
                    await tagCloud.InsertAsync(tile.Title, tile.Description, tile.SiteName);

                //TilesState.TagCloud = from entry in tagCloud.Cloud orderby entry.Key select entry;
                TilesState.TagCloud = tagCloud.Cloud;

                return await Unit.Task;
            }
        }
    }
}
