using System;
using System.IO;
using Ardalis.GuardClauses;
using Common.Extensions;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using static OpenGraphTilemaker.OpenGraph.CacheState;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace OpenGraphTilemaker.OpenGraph
{
    public class DiscCache
    {
        public DiscCache([NotNull] IOptions<DiscCacheOptions> options) {
            Guard.Against.Null(() => options);
            Options = Guard.Against.Null(() => options.Value);
        }

        public DiscCacheOptions Options { get; }

        public bool TryLoadFromDisc([NotNull] Uri uri, out string result) {
            Guard.Against.Null(() => uri);

            result = null;

            if (Options.CacheState != Enabled) return false;

            result = File.Exists(FullPath(uri)) ? File.ReadAllText(FullPath(uri)) : null;

            return result != null;
        }

        public string FullPath([NotNull] Uri uri) {
            Guard.Against.Null(() => uri);

            return Options.CacheFolder + uri.ToValidFileName();
        }

        public void WriteToDisc([NotNull] Uri uri, [NotNull] string html) {
            Guard.Against.Null(() => uri);
            Guard.Against.NullOrWhiteSpace(() => html);

            if (Options.CacheState == Enabled && html.NotNullNorWhiteSpace()) {
                File.WriteAllText(FullPath(uri), html);
            }
        }
    }
}
