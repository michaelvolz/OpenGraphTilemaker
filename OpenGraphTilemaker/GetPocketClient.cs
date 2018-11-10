using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.SyndicationFeed;

namespace OpenGraphTilemaker
{
    /// <summary>
    ///     GetPocket
    /// </summary>
    public class GetPocket
    {
        private readonly IMemoryCache _cache;

        public GetPocket(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public async Task<List<GetPocketEntry>> GetEntriesAsync(Uri uri, TimeSpan cachingTimeSpan)
        {
            if (cachingTimeSpan == default(TimeSpan))
                cachingTimeSpan = TimeSpan.FromHours(1);

            return await
                _cache.GetOrCreateAsync(CacheKeys.GetPocketFeed, async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = cachingTimeSpan;

                    var feedService = new FeedService<GetPocketEntry>(uri);
                    return await feedService.GetNewsFeedAsync(item => item.ToPocketEntry(), p => p.PubDate);
                });
        }
    }

    public class GetPocketEntry
    {
        public GetPocketEntry(string title, string category, string link, DateTime pubDate)
        {
            Title = title;
            Category = category;
            Link = link;
            PubDate = pubDate;
        }

        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
    }

    public static class SyndicationExtensions
    {
        public static GetPocketEntry ToPocketEntry(this ISyndicationItem item)
        {
            return new GetPocketEntry(item.Title,
                item.Categories.First().Name,
                pubDate: item.Published.UtcDateTime,
                link: item.Links.First().Uri.OriginalString);
        }
    }
}