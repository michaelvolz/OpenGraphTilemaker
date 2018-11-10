using System;

namespace OpenGraphTilemaker
{
    public class OpenGraphMetadata
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string SiteName { get; set; }
        public string Image { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string Locale { get; set; }

        public DateTime? ArticlePublishedTime { get; set; }
        public DateTime? ArticleModifiedTime { get; set; }

        public bool IsValid => !string.IsNullOrWhiteSpace(Title) &&
                               !string.IsNullOrWhiteSpace(Description) &&
                               !string.IsNullOrWhiteSpace(Image);

        public string Source { get; set; }
        public DateTime SourcePublishTime { get; set; }
    }
}