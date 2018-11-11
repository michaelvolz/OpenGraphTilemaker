using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;

namespace OpenGraphTilemaker.OpenGraph
{
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenGraphTileMaker _openGraphTileMaker;

        public TileMakerClient(HttpClient client, OpenGraphTileMaker tileMaker) {
            _httpClient = client;
            _openGraphTileMaker = tileMaker;
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri) {
            await _openGraphTileMaker.ScrapeAsync(_httpClient, uri);

            return _openGraphTileMaker.OpenGraphMetadata;
        }
    }
}