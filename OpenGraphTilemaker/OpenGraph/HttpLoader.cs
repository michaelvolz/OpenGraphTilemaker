using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using static System.Net.HttpStatusCode;

namespace OpenGraphTilemaker.OpenGraph
{
    public class HttpLoader
    {
        private readonly DiscCache _discCache;

        public HttpLoader(DiscCache cache) {
            _discCache = cache;
        }

        public async Task<HtmlDocument> LoadAsync(HttpClient httpClient, Uri uri, bool useCache = true) {
            string html = null;

            if (useCache)
                html = _discCache.TryLoad(uri);

            if (html == null) {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;

                if (httpStatusCode == Moved || httpStatusCode == MovedPermanently)
                    data = await httpClient.GetAsync(data.Headers.Location);

                html = await data.Content.ReadAsStringAsync();

                if (data.IsSuccessStatusCode && useCache && !string.IsNullOrWhiteSpace(html))
                    _discCache.WriteTo(uri, html);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }
    }
}