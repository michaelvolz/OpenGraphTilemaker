using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.OpenGraph
{
    public static class FileLoader
    {
        public static Task<HtmlDocument> LoadAsync([NotNull] string filePath) {
            if (filePath == null) throw new ArgumentNullException(nameof(filePath));
            
            var doc = new HtmlDocument();
            doc.Load(filePath);

            return Task.FromResult(doc);
        }
    }
}