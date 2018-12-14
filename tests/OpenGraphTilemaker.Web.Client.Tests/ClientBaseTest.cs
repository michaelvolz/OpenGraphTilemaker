using System;
using System.Net;
using System.Net.Http;
using BaseTestCode;
using BlazorState;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using Xunit.Abstractions;
using Options = Microsoft.Extensions.Options.Options;

// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable CheckNamespace

namespace OpenGraphTilemaker.Tests
{
    public class ClientBaseTest : BaseTest<ClientBaseTest>, IDisposable
    {
        private const string CachingFolder = @"C:\WINDOWS\Temp\";

        private static readonly Uri Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/all");
        protected static readonly TimeSpan CachingTimeSpan = TimeSpan.FromSeconds(120);
        protected static readonly TimeSpan TimeoutTimeSpan = TimeSpan.FromSeconds(15);

        private readonly HttpClient _realHttpClient;

        protected ClientBaseTest(ITestOutputHelper testConsole) : base(testConsole) {
            _realHttpClient = new HttpClient();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected static TileMakerClient TileMakerClient(string fakeResponse) {
            return new TileMakerClient(HttpClient(fakeResponse, HttpStatusCode.OK), TileMaker(), HttpLoader());
        }

        protected static OpenGraphTileMaker TileMaker() {
            return new OpenGraphTileMaker();
        }

        protected static HttpLoader HttpLoader() {
            return new HttpLoader(DiscCache());
        }

        protected static DiscCache DiscCache() {
            return new DiscCache(DiscCacheIOptions());
        }

        protected static IOptions<DiscCacheOptions> DiscCacheIOptions() {
            return Options.Create(new DiscCacheOptions { CacheFolder = CachingFolder, CacheState = CacheState.Disabled });
        }

        protected static IOptions<PocketOptions> GetPocketIOptions() {
            return Options.Create(new PocketOptions(Uri, CachingTimeSpan, TimeoutTimeSpan));
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

        protected TileMakerClient RealTileMakerClient() {
            return new TileMakerClient(_realHttpClient, TileMaker(), HttpLoader());
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                _realHttpClient?.Dispose();
            }
        }
    }
}
