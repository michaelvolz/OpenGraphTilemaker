﻿using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.SyndicationFeed;

namespace Domain.OpenGraphTilemaker.GetPocket
{
    public static class SyndicationExtensions
    {
        public static PocketEntry ToPocketEntry(this ISyndicationItem item)
        {
            Guard.Against.Null(item, nameof(item));

            return new PocketEntry(
                item.Title,
                item.Categories.FirstOrDefault()?.Name,
                pubDate: item.Published.UtcDateTime,
                link: item.Links.FirstOrDefault()?.Uri.OriginalString);
        }
    }
}
