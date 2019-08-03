using System;
using Ardalis.GuardClauses;
using Common;

namespace OpenGraphTilemaker.GetPocket
{
    public class PocketOptions : IPocketOptions, IVerifiableOptions
    {
        public PocketOptions() { }

        public PocketOptions( Uri uri, TimeSpan caching, TimeSpan timeout) {
            Uri = uri;
            CachingTimeSpan = caching;
            TimeOutTimeSpan = timeout;

            Verify();
        }

        public Uri? Uri { get; set; }
        public TimeSpan CachingTimeSpan { get; set; }
        public TimeSpan TimeOutTimeSpan { get; set; }

        public bool Verify() {
            Guard.Against.Null(() => Uri);
            Guard.Against.Default(() => CachingTimeSpan);
            Guard.Against.Default(() => TimeOutTimeSpan);

            return true;
        }
    }
}
