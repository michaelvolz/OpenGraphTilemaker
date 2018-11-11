using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

namespace OpenGraphTilemaker.OpenGraph
{
    // TODO: Split into 1) Load, 2) Extract and 3) Map
    public class OpenGraphTileMaker
    {
        private readonly OpenGraphTileMakerOptions _options;

        public IList<HtmlNode> HtmlMetaTags;
        public OpenGraphMetadata OpenGraphMetadata;

        public OpenGraphTileMaker([NotNull] IOptions<OpenGraphTileMakerOptions> options) {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (options.Value.CacheFolder == null) throw new ArgumentNullException(nameof(options.Value.CacheFolder));

            _options = options.Value;
        }

        public Exception Error { get; private set; }

        public async Task ScrapeAsync(HttpClient httpClient, Uri uri, bool useCache = true) {
            await ScrapeAsync(async () => await LoadWebAsync(httpClient, uri, useCache), uri.OriginalString);
        }

        public async Task ScrapeAsync(string filePath) {
            await ScrapeAsync(async () => await LoadFileAsync(filePath), filePath);
        }

        private void WriteToCache(Uri uri, string html) => File.WriteAllText(Filename(uri), html);

        private string TryLodFromCache(Uri uri) => File.Exists(Filename(uri)) ? File.ReadAllText(Filename(uri)) : null;

        private string Filename(Uri uri) => _options.CacheFolder + uri.ToValidFileName();

        private async Task ScrapeAsync(Func<Task<HtmlDocument>> loadDocument, string source) {
            try {
                var doc = await loadDocument();
                ExtractMetaData(doc, source);
            }
            catch (Exception e) {
                Error = e;
            }
        }

        private void ExtractMetaData(HtmlDocument doc, string source) {
            HtmlMetaTags = ExtractMetaTags(doc);
            OpenGraphMetadata = MapMetaData(HtmlMetaTags, source);
        }

        internal async Task<HtmlDocument> LoadWebAsync(HttpClient httpClient, Uri uri, bool useCache = false) {
            string html = null;
            if (useCache)
                html = TryLodFromCache(uri);

            if (html == null) {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;
                if (httpStatusCode == HttpStatusCode.Moved || httpStatusCode == HttpStatusCode.MovedPermanently) {
                    data = await httpClient.GetAsync(data.Headers.Location);
                }

                html = await data.Content.ReadAsStringAsync();

                if (data.IsSuccessStatusCode && useCache && !string.IsNullOrWhiteSpace(html))
                    WriteToCache(uri, html);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }

        private Task<HtmlDocument> LoadFileAsync(string filePath) {
            var doc = new HtmlDocument();
            doc.Load(filePath);

            return Task.FromResult(doc);
        }

        private static IList<HtmlNode> ExtractMetaTags(HtmlDocument doc) {
            var metaTags = doc.DocumentNode.SelectSingleNode("//head")?.Descendants()?.Where(n => n.Name == "meta");

            return metaTags?.ToList();
        }

        private OpenGraphMetadata MapMetaData(IList<HtmlNode> htmlMetaTags, string source) {
            var metadata = new OpenGraphMetadata {Source = source};

            if (htmlMetaTags == null)
                return metadata;

            foreach (var tag in htmlMetaTags) {
                var content = tag.GetAttributeValue("content", null);
                if (content == null)
                    continue;

                var property = tag.GetAttributeValue("property", null) ?? tag.GetAttributeValue("name", null);
                switch (property) {
                    case "og:type":
                        metadata.Type = content;
                        break;
                    case "og:title":
                        metadata.Title = content.DeEntitize();
                        break;
                    case "og:url":
                        metadata.Url = content;
                        break;
                    case "og:description":
                        metadata.Description = content.DeEntitize();
                        break;
                    case "og:site_name":
                        metadata.SiteName = content.DeEntitize();
                        break;
                    case "og:image":
                        metadata.Image = content;
                        break;
                    case "og:image:width":
                        metadata.ImageWidth = content.AsInt();
                        break;
                    case "og:image:height":
                        metadata.ImageHeight = content.AsInt();
                        break;
                    case "og:locale":
                        metadata.Locale = content;
                        break;
                    case "article:published_time":
                        metadata.ArticlePublishedTime = content.AsDateTime();
                        break;
                    case "article:modified_time":
                        metadata.ArticleModifiedTime = content.AsDateTime();
                        break;
                }
            }

            return metadata;
        }
    }
}