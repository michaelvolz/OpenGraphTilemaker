using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OpenGraphTilemaker
{
    public class TileMaker
    {
        private const string CacheFolder = @"C:\WINDOWS\Temp\";

        public IList<HtmlNode> HtmlMetaTags;
        public OpenGraphMetadata OpenGraphMetadata;

        public Exception Error { get; private set; }

        public async Task ScrapeAsync(HttpClient httpClient, Uri uri, bool useCache = true) =>
            await ScrapeAsync(async () => await LoadWebEnhanced(httpClient, uri, useCache));

        public async Task ScrapeHtml(string filePath) => await ScrapeAsync(() => LoadFileAsync(filePath));

        private async Task ScrapeAsync(Func<Task<HtmlDocument>> loadDocument)
        {
            try
            {
                var doc = await loadDocument();
                ExtractMetaData(doc);
            }
            catch (Exception e)
            {
                Error = e;
            }
        }

        private async Task<HtmlDocument> LoadFileAsync(string filePath)
        {
            await Task.FromResult(0);

            var doc = new HtmlDocument();
            doc.Load(filePath);

            return doc;
        }

        public async Task<HtmlDocument> LoadWebEnhanced(HttpClient httpClient, Uri uri, bool useCache = false)
        {
            string html = null;
            if (useCache)
                html = TryLodFromCache(uri);

            if (html == null)
            {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;
                if (httpStatusCode == HttpStatusCode.Moved || httpStatusCode == HttpStatusCode.MovedPermanently)
                {
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

        private static void WriteToCache(Uri uri, string html) => File.WriteAllText(Filename(uri), html);

        private string TryLodFromCache(Uri uri) => File.Exists(Filename(uri)) ? File.ReadAllText(Filename(uri)) : null;

        private static string Filename(Uri uri) => CacheFolder + uri.ToValidFileName();

        private void ExtractMetaData(HtmlDocument doc)
        {
            HtmlMetaTags = ExtractMetaTags(doc);
            OpenGraphMetadata = MapMetaData(HtmlMetaTags);
        }

        private OpenGraphMetadata MapMetaData(IList<HtmlNode> htmlMetaTags)
        {
            if (htmlMetaTags == null)
            {
                return null;
            }

            var metadata = new OpenGraphMetadata();

            foreach (var tag in htmlMetaTags)
            {
                var content = tag.GetAttributeValue("content", null);
                if (content == null)
                    continue;

                var property = tag.GetAttributeValue("property", null);
                switch (property)
                {
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

        private static IList<HtmlNode> ExtractMetaTags(HtmlDocument doc)
        {
            var metaTags = doc.DocumentNode.SelectSingleNode("//head")?.Descendants()?.Where(n => n.Name == "meta");

            return metaTags?.ToList();
        }
    }

    // ReSharper disable MemberCanBePrivate.Global
    public static class TileMakerExtensions
    {
        public static string TruncateAtWord(this string value, int length, string truncateAtChar = " ")
        {
            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            return $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length).Trim()} ...";
        }

        public static string ToValidFileName(this Uri uri) => uri.OriginalString.ToValidFileName();

        public static string ToValidFileName(this string name)
        {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var escapedInvalidChars = Regex.Escape(invalidChars);
            var invalidRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(name, invalidRegex, "_");
        }

        public static int? AsInt(this string value) => int.TryParse(value, out var result) ? (int?) result : null;

        public static DateTime? AsDateTime(this string value) =>
            DateTime.TryParse(value, out var result) ? (DateTime?) result : null;

        public static string DeEntitize(this string value) => HtmlEntity.DeEntitize(value);
    }
    // ReSharper restore MemberCanBePrivate.Global
}