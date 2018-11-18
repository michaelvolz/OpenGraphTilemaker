using System;
using System.ComponentModel;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.OpenGraph
{
    public class DiscCacheOptions
    {
        public DiscCacheOptions() { }

        public DiscCacheOptions([NotNull] string cacheFolder, CacheState cacheState) {
            if (!Enum.IsDefined(typeof(CacheState), cacheState))
                throw new InvalidEnumArgumentException(nameof(cacheState), (int) cacheState, typeof(CacheState));

            Guard.Against.Enum(() => cacheState, typeof(CacheState));

            CacheFolder = Guard.Against.NullOrWhiteSpace(() => cacheFolder);
            CacheState = Guard.Against.Default(() => cacheState);
        }

        public string CacheFolder { get; set; } = @"C:\WINDOWS\Temp\";
        public CacheState CacheState { get; set; } = CacheState.Enabled;
    }
}