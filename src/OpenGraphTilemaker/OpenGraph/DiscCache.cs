using System;
using System.IO;
using Ardalis.GuardClauses;
using Common.Extensions;
using Microsoft.Extensions.Options;
using static OpenGraphTilemaker.OpenGraph.CacheState;

namespace OpenGraphTilemaker.OpenGraph
{
    public class DiscCache
    {
        public DiscCache(IOptions<DiscCacheOptions> options)
        {
            Guard.Against.Null(options, nameof(options));

            DiscCacheOptions = Guard.Against.Null(options.Value, nameof(options.Value));
        }

        private DiscCacheOptions DiscCacheOptions { get; }

        public bool TryLoadFromDisc(Uri uri, out string? result)
        {
            Guard.Against.Null(uri, nameof(uri));

            result = null;

            if (DiscCacheOptions.CacheState != Enabled) return false;

            result = File.Exists(FullPath(uri)) ? File.ReadAllText(FullPath(uri)) : null;

            return result != null;
        }

        public void WriteToDisc(Uri uri, string html)
        {
            Guard.Against.Null(uri, nameof(uri));
            Guard.Against.NullOrWhiteSpace(() => html);

            if (DiscCacheOptions.CacheState == Enabled) File.WriteAllText(FullPath(uri), html);
        }

        private string FullPath(Uri uri)
        {
            Guard.Against.Null(uri, nameof(uri));

            return DiscCacheOptions.CacheFolder + uri.ToValidFileName();
        }
    }
}