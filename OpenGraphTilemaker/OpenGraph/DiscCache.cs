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
        private readonly DiscCacheOptions _options;

        public DiscCache([NotNull] IOptions<DiscCacheOptions> options) {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options.Value));
        }

        [CanBeNull]
        public string TryLoadFromDisc([NotNull] Uri uri) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return File.Exists(FullPath(uri)) ? File.ReadAllText(FullPath(uri)) : null;
        }

        public string FullPath([NotNull] Uri uri) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            return _options.CacheFolder + uri.ToValidFileName();
        }

        public void WriteToDisc([NotNull] Uri uri, string html) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (string.IsNullOrWhiteSpace(html)) throw new ArgumentException(nameof(html));

            File.WriteAllText(FullPath(uri), html);
        }
    }
}