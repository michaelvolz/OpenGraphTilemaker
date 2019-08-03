using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

#pragma warning disable SA1649 // File name should match first type name

namespace Common
{
    public class Feed<TEntry>
    {
        public async Task<List<TEntry>> GetFeedAsync(
             Uri uri,
             Func<ISyndicationItem, TEntry> convert,
             Func<TEntry, object> property,
            SortOrder order = SortOrder.Descending) {
            Guard.Against.Null(() => uri);
            Guard.Against.Null(() => convert);
            Guard.Against.Null(() => property);

            var feedItems = new List<TEntry>();

            using (var xmlReader = XmlReader.Create(uri.OriginalString, new XmlReaderSettings { Async = true })) {
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
