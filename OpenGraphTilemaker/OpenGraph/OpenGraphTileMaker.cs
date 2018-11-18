using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.OpenGraph
{
    public class OpenGraphTileMaker
    {
        public IList<HtmlNode> HtmlMetaTags;
        public OpenGraphMetadata OpenGraphMetadata;

        public Exception Error { get; private set; }

        public async Task ScrapeAsync([NotNull] string source, [NotNull] Func<Task<HtmlDocument>> loadDocument) {
            Guard.Against.NullOrWhiteSpace(() => source);
            Guard.Against.Null(() => loadDocument);

            try {
                var doc = await loadDocument();
                ExtractMetaData(doc, source);
            }
            catch (Exception e) {
                Error = e;
            }
        }

        private void ExtractMetaData(HtmlDocument doc, string source) {
            HtmlMetaTags = OpenGraphExtractor.ExtractMetaTags(doc);
            OpenGraphMetadata = OpenGraphMapper.MapMetaData(HtmlMetaTags, source);
        }
    }
}