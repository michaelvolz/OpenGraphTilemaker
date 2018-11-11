using System;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public class GetPocketOptions : IGetPocketOptions
    {
        public GetPocketOptions() { }

        public GetPocketOptions([NotNull] Uri uri, TimeSpan cachingTimeSpan) {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            if (cachingTimeSpan == default) throw new ArgumentOutOfRangeException(nameof(cachingTimeSpan));
            CachingTimeSpan = cachingTimeSpan;
        }

        public Uri Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
    }
}