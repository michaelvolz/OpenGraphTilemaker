using System;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions
    {
        public PocketOptions() { }

        public PocketOptions([NotNull] Uri uri, TimeSpan caching) {
            Uri = Guard.Against.Null(uri, nameof(uri));
            CachingTimeSpan = Guard.Against.Default(caching, nameof(caching));
        }

        public Uri Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
    }
}