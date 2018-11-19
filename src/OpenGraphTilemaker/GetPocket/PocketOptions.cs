using System;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions
    {
        public PocketOptions() { }

        public PocketOptions([NotNull] Uri uri, TimeSpan caching) {
            Uri = Guard.Against.Null(() => uri);
            CachingTimeSpan = Guard.Against.Default(() => caching);
        }

        public Uri Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
    }
}