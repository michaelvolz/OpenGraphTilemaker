using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using OpenGraphTilemaker;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : RequestHandler<SortTilesRequest, TilesState>
        {
            public SortTilesHandler(IStore store) : base(store)
            {
            }

            public TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SortTilesRequest request, CancellationToken token)
            {
                if (request.SortProperty != null)
                    TilesState.SortProperty = request.SortProperty;

                if (request.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = request.SortOrder;

                switch (TilesState.SortProperty)
                {
                    case nameof(OpenGraphMetadata.Title):
                        TilesState.Tiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.Tiles.OrderBy(f => f.Title).ToList()
                            : TilesState.Tiles.OrderByDescending(f => f.Title == null).ThenByDescending(f => f.Title).ToList();
                        break;

                    case nameof(OpenGraphMetadata.SourcePublishTime):
                        TilesState.Tiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.Tiles.OrderBy(f => f.SourcePublishTime).ToList()
                            : TilesState.Tiles.OrderByDescending(f => f.SourcePublishTime).ToList();
                        break;
                }

                return Task.FromResult(TilesState);
            }
        }
    }
}