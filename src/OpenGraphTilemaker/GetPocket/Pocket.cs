using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Microsoft.Extensions.Caching.Memory;

namespace OpenGraphTilemaker.GetPocket
{
    /// <summary>
    ///     GetPocket = https://app.getpocket.com/.
    /// </summary>
    public class Pocket : IPocket
    {
        private readonly Feed<PocketEntry> _feed;
        private readonly IMemoryCache _memoryCache;

        public Pocket(IMemoryCache memoryCache, Feed<PocketEntry> feed)
        {
            _memoryCache = Guard.Against.Null(() => memoryCache);
            _feed = Guard.Against.Null(() => feed);
        }

        public async Task<List<PocketEntry>> GetEntriesAsync(IPocketOptions options)
        {
            Guard.Against.Null(() => options.Uri);
            Guard.Against.Default(() => options.CachingTimeSpan);

            return await
                _memoryCache.GetOrCreateAsync(CacheKeys.GetPocketFeedKey, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = options.CachingTimeSpan;
                    return await _feed.GetFeedAsync(options.Uri!, item => item.ToPocketEntry(), p => p.PubDate);
                });
        }
    }
}