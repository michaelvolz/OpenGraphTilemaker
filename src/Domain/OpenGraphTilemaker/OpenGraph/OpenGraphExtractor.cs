using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using HtmlAgilityPack;

namespace Domain.OpenGraphTilemaker.OpenGraph
{
    public static class OpenGraphExtractor
    {
        public static IList<HtmlNode>? ExtractMetaTags(HtmlDocument doc)
        {
            Guard.Against.Null(doc, nameof(doc));

            var metaTags = doc.DocumentNode.SelectSingleNode("//head")?.Descendants()?.Where(n =>
                string.Equals(n.Name, "meta", StringComparison.InvariantCultureIgnoreCase));

            return metaTags?.ToList();
        }
    }
}
