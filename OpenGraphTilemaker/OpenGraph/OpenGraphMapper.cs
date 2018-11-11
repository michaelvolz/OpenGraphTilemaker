using System.Collections.Generic;
using HtmlAgilityPack;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class OpenGraphMapper
    {
        public static OpenGraphMetadata MapMetaData(IList<HtmlNode> htmlMetaTags, string source) {
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