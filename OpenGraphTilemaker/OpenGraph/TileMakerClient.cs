using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.OpenGraph
{
    // TODO: Extract helper and DiscCache methods
    [IoC]
    public class TileMakerClient : ITileMakerClient
    {
        private readonly HttpClient _httpClient;
        private readonly OpenGraphTileMaker _openGraphTileMaker;
        private readonly TileMakerClientOptions _tileMakerClientOptions;

        public TileMakerClient(HttpClient client, OpenGraphTileMaker tileMaker, IOptions<TileMakerClientOptions> options) {
            _httpClient = client;
            _openGraphTileMaker = tileMaker;
            _tileMakerClientOptions = options.Value;
        }

        public async Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri) {
            await _openGraphTileMaker.ScrapeAsync(async () => await LoadWebAsync(_httpClient, uri), uri.OriginalString);

            return _openGraphTileMaker.OpenGraphMetadata;
        }

        internal async Task<HtmlDocument> LoadWebAsync(HttpClient httpClient, Uri uri, bool useCache = true) {
            string html = null;
            if (useCache)
                html = TryLodFromDiscCache(uri);

            if (html == null) {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;
                if (httpStatusCode == HttpStatusCode.Moved || httpStatusCode == HttpStatusCode.MovedPermanently) {
                    data = await httpClient.GetAsync(data.Headers.Location);
                }

                html = await data.Content.ReadAsStringAsync();

                if (data.IsSuccessStatusCode && useCache && !string.IsNullOrWhiteSpace(html))
                    WriteToDiscCache(uri, html);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }

        private void WriteToDiscCache(Uri uri, string html) => File.WriteAllText(Filename(uri), html);

        private string TryLodFromDiscCache(Uri uri) => File.Exists(Filename(uri)) ? File.ReadAllText(Filename(uri)) : null;

        private string Filename(Uri uri) => _tileMakerClientOptions.CacheFolder + uri.ToValidFileName();
    }
}