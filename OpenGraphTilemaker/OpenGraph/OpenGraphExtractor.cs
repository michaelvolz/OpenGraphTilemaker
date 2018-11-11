using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class OpenGraphExtractor
    {
        public static IList<HtmlNode> ExtractMetaTags(HtmlDocument doc) {
            var metaTags = doc.DocumentNode.SelectSingleNode("//head")?.Descendants()?.Where(n => n.Name == "meta");

            return metaTags?.ToList();
        }
    }
}