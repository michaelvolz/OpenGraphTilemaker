using System;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class GetPocketEntry
    {
        public GetPocketEntry([NotNull] string title, string category, [NotNull] string link, DateTime pubDate) {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Category = category;
            Link = link ?? throw new ArgumentNullException(nameof(link));
            if (pubDate == default) throw new ArgumentOutOfRangeException(nameof(pubDate));
            PubDate = pubDate;
        }

        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
    }
}