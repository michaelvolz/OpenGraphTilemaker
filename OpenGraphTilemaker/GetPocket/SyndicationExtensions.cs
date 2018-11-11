using System.Linq;
using Microsoft.SyndicationFeed;

namespace OpenGraphTilemaker.GetPocket
{
    public static class SyndicationExtensions
    {
        public static GetPocketEntry ToPocketEntry(this ISyndicationItem item) {
            return new GetPocketEntry(item.Title,
                item.Categories.First().Name,
                pubDate: item.Published.UtcDateTime,
                link: item.Links.First().Uri.OriginalString);
        }
    }
}