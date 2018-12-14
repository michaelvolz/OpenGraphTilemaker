using System;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions
    {
        public PocketOptions() { }

        public PocketOptions([NotNull] Uri uri, TimeSpan caching, TimeSpan timeout) {
            Uri = Guard.Against.Null(() => uri);
            CachingTimeSpan = Guard.Against.Default(() => caching);
            TimeOutTimeSpan = Guard.Against.Default(() => timeout);
        }

        public Uri Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
        public TimeSpan TimeOutTimeSpan { get; set; }
    }
}
