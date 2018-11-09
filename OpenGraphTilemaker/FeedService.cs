using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace OpenGraphTilemaker
{
    public class FeedService<TEntry>
    {
        private readonly Uri _feedUri;

        public FeedService(Uri feedUri)
        {
            _feedUri = feedUri;
        }

        public async Task<List<TEntry>> GetNewsFeedAsync(
            Func<ISyndicationItem, TEntry> convert,
            Func<TEntry, object> property,
            SortOrder order = SortOrder.Descending)
        {
            var rssNewsItems = new List<TEntry>();
            using (var xmlReader = XmlReader.Create(_feedUri.OriginalString, new XmlReaderSettings {Async = true}))
            {
                var feedReader = new RssFeedReader(xmlReader);
                while (await feedReader.Read())
                {
                    if (feedReader.ElementType != SyndicationElementType.Item) continue;

                    var item = await feedReader.ReadItem();
                    rssNewsItems.Add(convert(item));
                }
            }

            return order == SortOrder.Ascending
                ? rssNewsItems.OrderBy(property).ToList()
                : rssNewsItems.OrderByDescending(property).ToList();
        }
    }
}