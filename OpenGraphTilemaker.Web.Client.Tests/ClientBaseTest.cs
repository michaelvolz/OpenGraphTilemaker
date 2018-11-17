using System;
using System.Net;
using BaseTestCode;
using BlazorState;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using Xunit.Abstractions;
using Options = Microsoft.Extensions.Options.Options;

// ReSharper disable ArgumentsStyleLiteral
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

namespace OpenGraphTilemaker.Tests
{
    public class ClientBaseTest : BaseTest
    {
        private static readonly Uri Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/all");
        private static readonly TimeSpan CachingTimeSpan = TimeSpan.FromSeconds(1);
        private static readonly string CachingFolder = @"C:\WINDOWS\Temp\";
        protected ClientBaseTest(ITestOutputHelper testConsole) : base(testConsole) { }

        protected static TileMakerClient TileMakerClient(string fakeResponse) {
            return new TileMakerClient(HttpClient(fakeResponse, HttpStatusCode.OK), TileMaker(), HttpLoader());
        }

        protected static OpenGraphTileMaker TileMaker() {
            return new OpenGraphTileMaker();
        }

        protected static HttpLoader HttpLoader() {
            return new HttpLoader(DiscCache(), CacheState.Disabled);
        }

        protected static DiscCache DiscCache() {
            return new DiscCache(DiscCacheIOptions());
        }

        protected static IOptions<DiscCacheOptions> DiscCacheIOptions() {
            return Options.Create(new DiscCacheOptions {CacheFolder = CachingFolder});
        }

        protected static IOptions<PocketOptions> GetPocketIOptions() {
            return Options.Create(new PocketOptions(Uri, CachingTimeSpan));
        }

        protected static Pocket Pocket() {
            return new Pocket(MemoryCache(), FeedService());
        }

        protected static Feed<PocketEntry> FeedService() {
            return new Feed<PocketEntry>();
        }

        protected static MemoryCache MemoryCache() {
            return new MemoryCache(new MemoryCacheOptions());
        }

        protected static MockStore MockStore(IState state) {
            var mockStore = new MockStore();
            mockStore.SetState(state);
            return mockStore;
        }
    }

    public class MockStore : IStore
    {
        private IState _state;

        public Guid Guid { get; } = Guid.NewGuid();

        public TState GetState<TState>() {
            return (TState) _state;
        }

        public void SetState(IState aState) {
            _state = aState;
        }
    }
}