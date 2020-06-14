using System.IO;
using Ardalis.GuardClauses;

namespace Domain.OpenGraphTilemaker.OpenGraph
{
    public class DiscCacheOptions
    {
        public DiscCacheOptions() { }

        public DiscCacheOptions(string cacheFolder, CacheState cacheState)
        {
            CacheFolder = Guard.Against.NullOrWhiteSpace(() => cacheFolder);
            Guard.Against.Enum(() => cacheState, typeof(CacheState));
            CacheState = Guard.Against.Default(() => cacheState);
        }

        public string CacheFolder { get; set; } = Path.GetTempPath();
        public CacheState CacheState { get; set; } = CacheState.Enabled;
    }
}
