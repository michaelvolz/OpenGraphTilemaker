using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using OpenGraphTilemaker.GetPocket;

namespace OpenGraphTilemaker.OpenGraph
{
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpLoader _httpLoader;
        private readonly OpenGraphTileMaker _openGraphTileMaker;

        public TileMakerClient(HttpClient httpClient, OpenGraphTileMaker openGraphTileMaker, HttpLoader httpLoader)
        {
            _httpClient = Guard.Against.Null(httpClient, nameof(httpClient));
            _openGraphTileMaker = Guard.Against.Null(openGraphTileMaker, nameof(openGraphTileMaker));
            _httpLoader = Guard.Against.Null(httpLoader, nameof(httpLoader));
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri, PocketEntry entry)
        {
            Guard.Against.Null(uri, nameof(uri));

            await _openGraphTileMaker.ScrapeAsync(uri.OriginalString, async () => await _httpLoader.LoadAsync(_httpClient, uri));

            var result = _openGraphTileMaker.GraphMetadata;
            result.BookmarkTime = entry.PubDate;

            return result;
        }
    }
}
