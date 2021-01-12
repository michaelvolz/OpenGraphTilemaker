using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Domain.OpenGraphTilemaker.GetPocket;

namespace Domain.OpenGraphTilemaker.OpenGraph
{
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpLoader _httpLoader;
        private readonly TileMaker _tileMaker;

        public TileMakerClient(HttpClient httpClient, TileMaker tileMaker, HttpLoader httpLoader)
        {
            _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
            _tileMaker = Guard.Against.Null(tileMaker, nameof(tileMaker));
            _httpLoader = Guard.Against.Null(httpLoader, nameof(httpLoader));
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri, PocketEntry entry)
        {
            Guard.Against.Null(uri, nameof(uri));
            Guard.Against.Null(entry, nameof(entry));

            await _tileMaker.ScrapeAsync(uri.OriginalString, async () => await _httpLoader.LoadAsync(_httpClient, uri));

            var result = _tileMaker.GraphMetadata;
            result.BookmarkTime = entry.PubDate;

            return result;
        }
    }
}
