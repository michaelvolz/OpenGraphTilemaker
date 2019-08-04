using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using OpenGraphTilemaker.GetPocket;

// ReSharper disable MemberCanBePrivate.Global

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
            _httpClient = Guard.Against.Null(() => httpClient);
            _openGraphTileMaker = Guard.Against.Null(() => openGraphTileMaker);
            _httpLoader = Guard.Against.Null(() => httpLoader);
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri, PocketEntry entry)
        {
            Guard.Against.Null(() => uri);

            await _openGraphTileMaker.ScrapeAsync(uri.OriginalString,
                async () => await _httpLoader.LoadAsync(_httpClient, uri));

            var result = _openGraphTileMaker.GraphMetadata;

            if (result != null) result.BookmarkTime = entry.PubDate;

            return result ?? new OpenGraphMetadata();
        }
    }
}