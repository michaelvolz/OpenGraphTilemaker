using System;
using Common.Extensions;

namespace OpenGraphTilemaker.OpenGraph
{
    public class OpenGraphMetadata
    {
        public string? Type { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string Description { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string? Locale { get; set; }

        public DateTime? ArticlePublishedTime { get; set; }
        public DateTime? ArticleModifiedTime { get; set; }

        public bool IsValid => Title.NotNullNorWhiteSpace() &&
                               Description.NotNullNorWhiteSpace() &&
                               Image.NotNullNorWhiteSpace();

        public string? Source { get; set; }
        public DateTime BookmarkTime { get; set; }
    }
}