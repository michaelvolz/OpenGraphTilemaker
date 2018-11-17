using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using JetBrains.Annotations;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.OpenGraph
{
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenGraphTileMaker _openGraphTileMaker;
        private readonly HttpLoader _webLoader;

        public TileMakerClient([NotNull] HttpClient client, [NotNull] OpenGraphTileMaker tileMaker, [NotNull] HttpLoader loader) {
            _httpClient = client ?? throw new ArgumentNullException(nameof(client));
            _openGraphTileMaker = tileMaker ?? throw new ArgumentNullException(nameof(tileMaker));
            _webLoader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync([NotNull] Uri uri) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            await _openGraphTileMaker.ScrapeAsync(uri.OriginalString, async () => await _webLoader.LoadAsync(_httpClient, uri));

            return _openGraphTileMaker.OpenGraphMetadata;
        }
    }
}