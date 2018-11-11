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
    public class GetPocket : IGetPocket
    {
        private readonly FeedService<GetPocketEntry> _feedService;
        private readonly IMemoryCache _memoryCache;

        public GetPocket([NotNull] IMemoryCache memoryCache, [NotNull] FeedService<GetPocketEntry> feedService) {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _feedService = feedService ?? throw new ArgumentNullException(nameof(feedService));
        }

        public async Task<List<GetPocketEntry>> GetEntriesAsync(IGetPocketOptions options) {
            if (options.Uri == null) throw new ArgumentNullException(nameof(options.Uri));
            if (options.CachingTimeSpan == default) throw new ArgumentOutOfRangeException(nameof(options.CachingTimeSpan));

            return await
                _memoryCache.GetOrCreateAsync(CacheKeys.GetPocketFeed, async entry => {
                    entry.AbsoluteExpirationRelativeToNow = options.CachingTimeSpan;
                    return await _feedService.GetFeedAsync(options.Uri, item => item.ToPocketEntry(), p => p.PubDate);
                });
        }
    }
}