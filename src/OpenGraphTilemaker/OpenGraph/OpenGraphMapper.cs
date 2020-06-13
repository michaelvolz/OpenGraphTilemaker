using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Common.Extensions;
using HtmlAgilityPack;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class OpenGraphMapper
    {
        public static OpenGraphMetadata MapMetaData(IList<HtmlNode>? htmlMetaTags, string source)
        {
            Guard.Against.NullOrWhiteSpace(() => source);

            var metadata = new OpenGraphMetadata { Source = source };

            if (htmlMetaTags == null)
                return metadata;

            foreach (var tag in htmlMetaTags)
            {
                var content = tag.GetAttributeValue("content", null);
                if (content == null)
                    continue;

                var property = tag.GetAttributeValue("property", null) ?? tag.GetAttributeValue("name", null);
                MapValues(property, metadata, content);
            }

            return metadata;
        }

        [SuppressMessage(
            "StyleCop.CSharp.NamingRules", "SA1312:Variable names should begin with lower-case letter", Justification = "Discarded variable syntax")]
        private static void MapValues(string property, OpenGraphMetadata metadata, string content)
        {
            object? _ = property switch
            {
                "og:type" => metadata.Type = content,
                "og:title" => metadata.Title = content.DeEntitize(),
                "og:url" => metadata.Url = content,
                "og:description" => metadata.Description = content.DeEntitize(),
                "og:site_name" => metadata.SiteName = content.DeEntitize(),
                "og:image" => metadata.Image = content,
                "og:image:width" => metadata.ImageWidth = content.AsIntOrNull(),
                "og:image:height" => metadata.ImageHeight = content.AsIntOrNull(),
                "og:locale" => metadata.Locale = content,
                "article:published_time" => metadata.ArticlePublishedTime = content.AsDateTimeOrNull(),
                "article:modified_time" => metadata.ArticleModifiedTime = content.AsDateTimeOrNull(),
                _ => string.Empty
            };
        }
    }
}
