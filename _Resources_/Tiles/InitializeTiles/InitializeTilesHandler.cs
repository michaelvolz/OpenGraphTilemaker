//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using BlazorState;
//using Common;
//using JetBrains.Annotations;
//using Microsoft.Extensions.Caching.Memory;
//using Microsoft.Extensions.DependencyInjection;
//using OpenGraphTilemaker;
//using OpenGraphTilemakerWeb.App.Features;
//
//// ReSharper disable MemberCanBePrivate.Global
//
//namespace OpenGraphTilemakerWeb.App.Pages.Tiles
//{
//    public partial class TilesState
//    {
//        [IoC]
//        public class InitializeTilesHandler : RequestHandler<InitializeTilesRequest, TilesState>
//        {
//            public InitializeTilesHandler([NotNull] IStore store) : base(store)
//            {
//                if (store == null) throw new ArgumentNullException(nameof(store));
//            }
//
//            public TilesState TilesState => Store.GetState<TilesState>();
//
//            public override Task<TilesState> Handle(InitializeTilesRequest request,
//                CancellationToken token)
//            {
//                return Task.FromResult(TilesState);
//
//                var memoryCache = ServiceProviderFactory.ServiceProvider.GetService<IMemoryCache>();
//                var tileMakerClient = ServiceProviderFactory.ServiceProvider.GetService<ITileMakerClient>();
//
//                var pocket = new GetPocket(memoryCache);
//                var urls = pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/"),
//                    TimeSpan.FromSeconds(15)).GetAwaiter().GetResult();
//
//                foreach (var pocketEntry in urls)
//                {
//                    var openGraphMetadata = tileMakerClient.OpenGraphMetadataAsync(new Uri(pocketEntry.Link))
//                        .GetAwaiter().GetResult();
//
//                    if (openGraphMetadata == null) continue;
//
//                    openGraphMetadata.SourcePublishTime = pocketEntry.PubDate;
//                    TilesState.Tiles.Add(openGraphMetadata);
//                }
//
//                return Task.FromResult(TilesState);
//            }
//        }
//    }
//}