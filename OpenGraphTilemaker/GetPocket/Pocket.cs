using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Memory;

namespace OpenGraphTilemaker.GetPocket
{
    /// <summary>
    ///     GetPocket = https://app.getpocket.com/
    /// </summary>
    public class Pocket : IPocket
    {
        private readonly Feed<PocketEntry> _feed;
        private readonly IMemoryCache _memoryCache;

        public Pocket([NotNull] IMemoryCache memoryCache, [NotNull] Feed<PocketEntry> feed) {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _feed = feed ?? throw new ArgumentNullException(nameof(feed));
        }

        public async Task<List<PocketEntry>> GetEntriesAsync(IPocketOptions options) {
            if (options.Uri == null) throw new ArgumentNullException(nameof(options.Uri));
            if (options.CachingTimeSpan == default) throw new ArgumentOutOfRangeException(nameof(options.CachingTimeSpan));

            return await
                _memoryCache.GetOrCreateAsync(CacheKeys.GetPocketFeed, async entry => {
                    entry.AbsoluteExpirationRelativeToNow = options.CachingTimeSpan;
                    return await _feed.GetFeedAsync(options.Uri, item => item.ToPocketEntry(), p => p.PubDate);
                });
        }
    }
}