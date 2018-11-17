using System;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions
    {
        public PocketOptions() { }

        public PocketOptions([NotNull] Uri uri, TimeSpan caching) {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            CachingTimeSpan = caching != default ? caching : throw new ArgumentException(nameof(caching));
        }

        public Uri Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
    }
}