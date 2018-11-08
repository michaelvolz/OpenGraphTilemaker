using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace OpenGraphTilemaker
{
    public class TileMaker
    {
        public IList<HtmlNode> HtmlMetaTags;
        public OpenGraphMetadata OpenGraphMetadata;

        public Exception Error { get; private set; }

        public async Task ScrapeHtmlAsync(HttpClient httpClient, Uri uri, bool useCache = true) =>
            await ScrapeHtmlGenericAsync(async () => await LoadWebEnhanced(httpClient, uri, useCache));

        public void ScrapeHtml(Uri uri, bool useCache = true) => ScrapeHtmlGeneric(() => LoadWeb(uri, useCache));

        public void ScrapeHtml(string filePath) => ScrapeHtmlGeneric(() => LoadFile(filePath));

        private async Task ScrapeHtmlGenericAsync(Func<Task<HtmlDocument>> loadDocument)
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

        private void ScrapeHtmlGeneric(Func<HtmlDocument> loadDocument)
        {
            try
            {
                var doc = loadDocument();
                ExtractMetaData(doc);
            }
            catch (Exception e)
            {
                Error = e;
            }
        }

        private HtmlDocument LoadFile(string filePath)
        {
            var doc = new HtmlDocument();
            doc.Load(filePath);
            return doc;
        }

        private HtmlDocument LoadWeb(Uri uri, bool useCache)
        {
            var web = new HtmlWeb
            {
                CaptureRedirect = true,
                UseCookies = true,
                CachePath = @"C:\WINDOWS\Temp\",
                UsingCache = useCache,
                UsingCacheIfExists = true,
                UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"
            };
            var doc = web.Load(uri);
            return doc;
        }

        public async Task<HtmlDocument> LoadWebEnhanced(HttpClient httpClient, Uri uri, bool useCache = false)
        {
            {
                {
                    var data = await httpClient.GetAsync(uri);

                    var isSuccess = data.IsSuccessStatusCode;
                    var httpStatusCode = data.StatusCode;

                    if (httpStatusCode == HttpStatusCode.Moved || httpStatusCode == HttpStatusCode.MovedPermanently)
                    {
                        data = await httpClient.GetAsync(data.Headers.Location);
                    }

                    var doc = new HtmlDocument();
                    var html = await data.Content.ReadAsStringAsync();
                    doc.LoadHtml(html);
                    return doc;
                }
            }

//            using (HttpClientHandler handler = new HttpClientHandler())
//            {
//                handler.AllowAutoRedirect = true;
//                using (HttpClient httpClient = new HttpClient(handler))
//                {
//                    var data = await httpClient.GetAsync(uri);
//
//                    var isSuccess = data.IsSuccessStatusCode;
//                    var httpStatusCode = data.StatusCode;
//
//                    if (httpStatusCode == HttpStatusCode.Moved || httpStatusCode == HttpStatusCode.MovedPermanently)
//                    {
//                        data = await httpClient.GetAsync(data.Headers.Location);
//                    }
//
//                    var doc = new HtmlDocument();
//                    var html = await data.Content.ReadAsStringAsync();
//                    doc.LoadHtml(html);
//                    return doc;
//                }
//            }
        }

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

    public static class TileMakerExtensions
    {
        public static int? AsInt(this string value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return null;
        }

        public static DateTime? AsDateTime(this string value)
        {
            if (DateTime.TryParse(value, out var result))
                return result;

            return null;
        }

        public static string DeEntitize(this string value)
        {
            return HtmlEntity.DeEntitize(value);
        }
    }
}