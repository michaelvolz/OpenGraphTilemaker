﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HtmlAgilityPack;

namespace OpenGraphTilemaker.OpenGraph
{
    public class OpenGraphTileMaker
    {
        public IList<HtmlNode>? HtmlMetaTags { get; private set; }
        public OpenGraphMetadata GraphMetadata { get; private set; } = new OpenGraphMetadata();

        public Exception? Error { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "By Design")]
        public async Task ScrapeAsync(string source, Func<Task<HtmlDocument>> loadDocumentAsync)
        {
            source = Guard.Against.NullOrWhiteSpace(() => source);
            loadDocumentAsync = Guard.Against.Null(() => loadDocumentAsync);

            try
            {
                var doc = await loadDocumentAsync();
                ExtractMetaData(doc, source);
            }
            catch (Exception e)
            {
                Error = e;
            }
        }

        private void ExtractMetaData(HtmlDocument doc, string source)
        {
            HtmlMetaTags = OpenGraphExtractor.ExtractMetaTags(doc);
            GraphMetadata = OpenGraphMapper.MapMetaData(HtmlMetaTags, source);
        }
    }
}