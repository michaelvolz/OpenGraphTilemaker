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
        public async Task<List<TEntry>> GetFeedAsync(Uri uri, Func<ISyndicationItem, TEntry> convert, Func<TEntry, object> property,
            SortOrder order = SortOrder.Descending) {
            var feedItems = new List<TEntry>();

            using (var xmlReader = XmlReader.Create(uri.OriginalString, new XmlReaderSettings {Async = true})) {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read()) {
                    if (feedReader.ElementType != SyndicationElementType.Item) continue;

                    var item = await feedReader.ReadItem();
                    feedItems.Add(convert(item));
                }
            }

            return order == SortOrder.Ascending
                ? feedItems.OrderBy(property).ToList()
                : feedItems.OrderByDescending(property).ToList();
        }
    }
}