using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.OpenGraph
{
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenGraphTileMaker _openGraphTileMaker;
        private readonly HttpLoader _webLoader;

        public TileMakerClient(HttpClient client, OpenGraphTileMaker tileMaker, HttpLoader loader) {
            _httpClient = client;
            _openGraphTileMaker = tileMaker;
            _webLoader = loader;
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri) {
            await _openGraphTileMaker.ScrapeAsync(uri.OriginalString, async () => await _webLoader.LoadAsync(_httpClient, uri));

            return _openGraphTileMaker.OpenGraphMetadata;
        }
    }
}