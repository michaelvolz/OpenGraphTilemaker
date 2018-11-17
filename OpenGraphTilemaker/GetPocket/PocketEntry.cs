using System;
using Common.Extensions;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketEntry
    {
        public PocketEntry([NotNull] string title, string category, [CanBeNull] string link, DateTime pubDate) {
            Title = title.NotNullOrWhiteSpace() ? title : throw new ArgumentException(nameof(title));
            Category = category ?? string.Empty;
            Link = link.NotNullOrWhiteSpace() ? link : throw new ArgumentException(nameof(link));
            PubDate = pubDate != default ? pubDate : throw new ArgumentException(nameof(pubDate));
        }

        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
    }
}