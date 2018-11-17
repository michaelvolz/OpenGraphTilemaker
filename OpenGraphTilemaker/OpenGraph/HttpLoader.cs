using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.Extensions;
using HtmlAgilityPack;
using JetBrains.Annotations;
using static System.Net.HttpStatusCode;
using static OpenGraphTilemaker.OpenGraph.CacheState;

namespace OpenGraphTilemaker.OpenGraph
{
    public class HttpLoader
    {
        private readonly CacheState _cacheState;
        private readonly DiscCache _discCache;

        public HttpLoader([NotNull] DiscCache discCache, CacheState cacheState = Enabled) {
            _discCache = discCache ?? throw new ArgumentNullException(nameof(discCache));
            _cacheState = cacheState != default ? cacheState : throw new ArgumentException(nameof(cacheState));
        }

        public async Task<HtmlDocument> LoadAsync([NotNull] HttpClient httpClient, [NotNull] Uri uri) {
            if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            string html = null;

            if (_cacheState == Enabled)
                html = _discCache.TryLoadFromDisc(uri);

            if (html == null) {
                var data = await httpClient.GetAsync(uri);
                var httpStatusCode = data.StatusCode;

                if (httpStatusCode == Moved || httpStatusCode == MovedPermanently)
                    data = await httpClient.GetAsync(data.Headers.Location);

                html = await data.Content.ReadAsStringAsync();

                if (data.IsSuccessStatusCode && _cacheState == Enabled && html.NotNullNorWhiteSpace())
                    _discCache.WriteToDisc(uri, html);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return doc;
        }
    }
}