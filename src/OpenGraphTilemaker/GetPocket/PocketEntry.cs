using System;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketEntry
    {
        public PocketEntry( string title, string category,  string link, DateTime pubDate) {
            Title = Guard.Against.NullOrWhiteSpace(() => title);
            Category = category ?? string.Empty;
            Link = Guard.Against.NullOrWhiteSpace(() => link);
            PubDate = Guard.Against.Default(() => pubDate);
        }

        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
    }
}
