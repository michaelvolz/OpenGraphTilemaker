using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace OpenGraphTilemaker
{
    public class TileMaker
    {
        public IList<HtmlNode> HtmlMetaTags;
        public OpenGraphMetadata OpenGraphMetadata;
        public Exception Error { get; private set; }

        public void ScrapeHtml(Uri uri)
        {
            try
            {
                var web = new HtmlWeb
                {
                    CaptureRedirect = true,
                    UseCookies = true,
                    CachePath = @"C:\WINDOWS\Temp\",
                    UsingCache = true,
                    UsingCacheIfExists = true,
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"
                };
                var doc = web.Load(uri);
                ExtractMetaData(doc);
            }
            catch (Exception e)
            {
                Error = e;
            }
        }

        public void ScrapeHtml(string filePath)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.Load(filePath);
                ExtractMetaData(doc);
            }
            catch (Exception e)
            {
                Error = e;
            }
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