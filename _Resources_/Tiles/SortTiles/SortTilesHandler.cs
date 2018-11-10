//using System.Threading;
//using System.Threading.Tasks;
//using BlazorState;
//using Common;
//
//// ReSharper disable MemberCanBePrivate.Global
//
//namespace OpenGraphTilemakerWeb.App.Pages.Tiles
//{
//    public partial class TilesState
//    {
//        [IoC]
//        public class SortTilesHandler : RequestHandler<SortTilesRequest, TilesState>
//        {
//            public SortTilesHandler(IStore store) : base(store)
//            {
//            }
//
//            public TilesState TilesState => Store.GetState<TilesState>();
//
//            public override Task<TilesState> Handle(SortTilesRequest sortTilesRequest,
//                CancellationToken cancellationToken)
//            {
////                TilesState.Count += SortTilesRequest.Amount;
//                return Task.FromResult(TilesState);
//            }
//        }
//    }
//}