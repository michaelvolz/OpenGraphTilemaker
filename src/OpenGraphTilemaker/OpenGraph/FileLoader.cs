using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HtmlAgilityPack;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class FileLoader
    {
        public static Task<HtmlDocument> LoadAsync(string filePath)
        {
            Guard.Against.NullOrWhiteSpace(() => filePath);

            var doc = new HtmlDocument();
            doc.Load(filePath);

            return MethodNeedsToReturnTask(doc);
        }

        private static Task<HtmlDocument> MethodNeedsToReturnTask(HtmlDocument doc) => Task.FromResult(doc);
    }
}
