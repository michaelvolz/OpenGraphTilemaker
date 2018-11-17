using System;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketEntry
    {
        public PocketEntry([NotNull] string title, string category, [CanBeNull] string link, DateTime pubDate) {
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException(nameof(title));
            Category = category ?? string.Empty;
            Link = !string.IsNullOrWhiteSpace(link) ? link : throw new ArgumentException(nameof(link));
            PubDate = pubDate != default ? pubDate : throw new ArgumentException(nameof(pubDate));
        }

        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
    }
}