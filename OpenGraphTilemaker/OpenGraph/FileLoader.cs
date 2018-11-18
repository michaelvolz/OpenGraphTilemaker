using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class FileLoader
    {
        public static Task<HtmlDocument> LoadAsync([NotNull] string filePath) {
            Guard.Against.NullOrWhiteSpace(filePath, nameof(filePath));

            var doc = new HtmlDocument();
            doc.Load(filePath);

            return Task.FromResult(doc);
        }
    }
}