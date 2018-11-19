using System.Linq;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.SyndicationFeed;

namespace OpenGraphTilemaker.GetPocket
{
    public static class SyndicationExtensions
    {
        public static PocketEntry ToPocketEntry([NotNull] this ISyndicationItem item) {
            Guard.Against.Null(() => item);

            return new PocketEntry(
                item.Title,
                item.Categories.First().Name,
                pubDate: item.Published.UtcDateTime,
                link: item.Links.First().Uri.OriginalString);
        }
    }
}