using System;
using System.IO;
using Common.Extensions;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace OpenGraphTilemaker.OpenGraph
{
    public class DiscCache
    {
        private readonly DiscCacheOptions _discCacheOptions;

        public DiscCache(IOptions<DiscCacheOptions> options) {
            _discCacheOptions = options.Value;
        }

        public void WriteTo(Uri uri, string html) => File.WriteAllText(Filename(uri), html);

        [CanBeNull]
        public string TryLoad(Uri uri) => File.Exists(Filename(uri)) ? File.ReadAllText(Filename(uri)) : null;

        public string Filename(Uri uri) => _discCacheOptions.CacheFolder + uri.ToValidFileName();
    }
}