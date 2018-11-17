using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentException(nameof(source));
            if (loadDocument == null) throw new ArgumentNullException(nameof(loadDocument));

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