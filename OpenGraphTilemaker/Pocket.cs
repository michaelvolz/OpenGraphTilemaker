using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SyndicationFeed;

namespace OpenGraphTilemaker
{
    //https://getpocket.com/users/Flynn0r/feed/all
    public class Pocket
    {
        public async Task<List<PocketEntry>> GetEntriesAsync(Uri uri)
        {
            var service = new NewsFeedService<PocketEntry>(uri);
            var newsFeed = await service.GetNewsFeedAsync(item => item.ToPocketEntry(), p => p.PubDate);
            return newsFeed;
        }
    }

    public class PocketEntry
    {
        public PocketEntry(string title, string category, string link, DateTime pubDate)
        {
            Title = title;
            Category = category;
            Link = link;
            PubDate = pubDate;
        }

        // ReSharper disable MemberCanBePrivate.Global
        public string Title { get; }
        public string Category { get; }
        public string Link { get; }

        public DateTime PubDate { get; }
        // ReSharper restore MemberCanBePrivate.Global
    }

    //Extension Methods for converting a ISyndicationItem to a PocketEntry
    public static class SyndicationExtensions
    {
        public static PocketEntry ToPocketEntry(this ISyndicationItem item)
        {
            return new PocketEntry(item.Title,
                item.Categories.First().Name,
                pubDate: item.Published.UtcDateTime,
                link: item.Links.First().Uri.OriginalString);
        }
    }
}