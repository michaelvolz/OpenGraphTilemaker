using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using JetBrains.Annotations;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;

namespace Common
{
    public class Feed<TEntry>
    {
        public async Task<List<TEntry>> GetFeedAsync(
            [NotNull] Uri uri,
            [NotNull] Func<ISyndicationItem, TEntry> convert,
            [NotNull] Func<TEntry, object> property,
            SortOrder order = SortOrder.Descending) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (convert == null) throw new ArgumentNullException(nameof(convert));
            if (property == null) throw new ArgumentNullException(nameof(property));

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