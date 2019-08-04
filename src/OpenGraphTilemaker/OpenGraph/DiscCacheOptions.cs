using System.IO;
using Ardalis.GuardClauses;

namespace OpenGraphTilemaker.OpenGraph
{
    public class DiscCacheOptions
    {
        public DiscCacheOptions() { }

        public DiscCacheOptions(string cacheFolder, CacheState cacheState)
        {
            Guard.Against.Enum(() => cacheState, typeof(CacheState));

            CacheFolder = Guard.Against.NullOrWhiteSpace(() => cacheFolder);
            CacheState = Guard.Against.Default(() => cacheState);
        }

        public string CacheFolder { get; set; } = Path.GetTempPath();
        public CacheState CacheState { get; set; } = CacheState.Enabled;
    }
}