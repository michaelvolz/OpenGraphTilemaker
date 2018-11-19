using System.Collections.Generic;
using Ardalis.GuardClauses;
using Common.Extensions;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class OpenGraphMapper
    {
        public static OpenGraphMetadata MapMetaData([CanBeNull] IList<HtmlNode> htmlMetaTags, [NotNull] string source) {
            Guard.Against.NullOrWhiteSpace(() => source);

            var metadata = new OpenGraphMetadata { Source = source };

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
                        metadata.ImageWidth = content.AsIntOrNull();
                        break;
                    case "og:image:height":
                        metadata.ImageHeight = content.AsIntOrNull();
                        break;
                    case "og:locale":
                        metadata.Locale = content;
                        break;
                    case "article:published_time":
                        metadata.ArticlePublishedTime = content.AsDateTimeOrNull();
                        break;
                    case "article:modified_time":
                        metadata.ArticleModifiedTime = content.AsDateTimeOrNull();
                        break;
                }
            }

            return metadata;
        }
    }
}