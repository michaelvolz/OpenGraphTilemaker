using System;
using Ardalis.GuardClauses;
using Common;

namespace Domain.OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions
    {
        [IoC]
        public PocketOptions() { }

        public PocketOptions(Uri uri, TimeSpan caching, TimeSpan timeout)
        {
            Guard.Against.Null(uri, nameof(uri));
            Guard.Against.Default(() => caching);
            Guard.Against.Default(() => timeout);

            Uri = uri;
            CachingTimeSpan = caching;
            TimeOutTimeSpan = timeout;
        }

        public Uri? Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
        public TimeSpan TimeOutTimeSpan { get; set; }
    }
}
