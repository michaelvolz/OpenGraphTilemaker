using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using BaseTestCode;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenGraphTilemaker.GetPocket;
using OpenGraphTilemaker.OpenGraph;
using Xunit.Abstractions;

// ReSharper disable once CheckNamespace
namespace OpenGraphTilemaker.Tests
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class IntegrationTests<T> : BaseTest<T>
    {
        private const string CachingFolder = @"C:\WINDOWS\Temp\";

        private readonly TimeSpan _cachingTimeSpan = TimeSpan.FromSeconds(120);

        private readonly HttpClient _realHttpClient;
        private readonly TimeSpan _timeoutTimeSpan = TimeSpan.FromSeconds(15);
        private readonly Uri _uri = new Uri("https://getpocket.com/users/Flynn0r/feed/all");
        private MemoryCache? _memoryCache;

        protected IntegrationTests(ITestOutputHelper testConsole) : base(testConsole) => _realHttpClient = new HttpClient();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _realHttpClient.Dispose();
                _memoryCache?.Dispose();
            }

            base.Dispose(disposing);
        }

        protected HttpLoader HttpLoader() => new HttpLoader(DiscCache());

        protected OpenGraphTileMaker TileMaker() => new OpenGraphTileMaker();

        protected DiscCache DiscCache() => new DiscCache(DiscCacheIOptions());

        protected Pocket Pocket() => new Pocket(MemoryCache(), FeedService());

        protected Feed<PocketEntry> FeedService() => new Feed<PocketEntry>();

        protected MemoryCache MemoryCache()
        {
            _memoryCache ??= new MemoryCache(new MemoryCacheOptions());
            
            return _memoryCache;
        }

        protected TileMakerClient RealTileMakerClient() => new TileMakerClient(_realHttpClient, TileMaker(), HttpLoader());

        protected IOptions<PocketOptions> GetPocketIOptions() => Options.Create(new PocketOptions(_uri, _cachingTimeSpan, _timeoutTimeSpan));

        protected IOptions<DiscCacheOptions> DiscCacheIOptions() =>
            Options.Create(new DiscCacheOptions {CacheFolder = CachingFolder, CacheState = CacheState.Disabled});

        protected TileMakerClient TileMakerClient(string fakeResponse) =>
            new TileMakerClient(HttpClient(fakeResponse), TileMaker(), HttpLoader());
    }
}