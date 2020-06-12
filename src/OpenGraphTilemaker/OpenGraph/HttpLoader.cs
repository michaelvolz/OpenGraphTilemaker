using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using HtmlAgilityPack;
using static System.Net.HttpStatusCode;

namespace OpenGraphTilemaker.OpenGraph
{
    public class HttpLoader
    {
        private readonly DiscCache _discCache;

        public HttpLoader(DiscCache discCache) => _discCache = Guard.Against.Null(discCache, nameof(discCache));

        public async Task<HtmlDocument> LoadAsync(HttpClient httpClient, Uri uri)
        {
            Guard.Against.Null(httpClient, nameof(httpClient));
            Guard.Against.Null(uri, nameof(uri));

            var loadedFromCache = _discCache.TryLoadFromDisc(uri, out var html);

            if (!loadedFromCache)
            {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;

                if (httpStatusCode == Moved || httpStatusCode == MovedPermanently)
                    data = await httpClient.GetAsync(data.Headers.Location);

                html = await data.Content.ReadAsStringAsync();

                if (data.IsSuccessStatusCode)
                    _discCache.WriteToDisc(uri, html);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }
    }
}
