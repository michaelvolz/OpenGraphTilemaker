using System;
using System.Diagnostics.CodeAnalysis;
using Common.Extensions;

namespace OpenGraphTilemaker.OpenGraph
{
    public class OpenGraphMetadata
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        [SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "HtmlAgilityPack does not work with Uri")]
        public string Url { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string Locale { get; set; } = string.Empty;

        public DateTime? ArticlePublishedTime { get; set; }
        public DateTime? ArticleModifiedTime { get; set; }

        public bool IsValid => Title.NotNullNorWhiteSpace() &&
                               Description.NotNullNorWhiteSpace() &&
                               Image.NotNullNorWhiteSpace();

        public string Source { get; set; } = string.Empty;
        public DateTime BookmarkTime { get; set; }
    }
}
