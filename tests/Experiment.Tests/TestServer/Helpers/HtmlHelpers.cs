using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;

namespace Experiment.Tests.TestServer.Helpers
{
    public static class HtmlHelpers
    {
        public static async Task<IHtmlDocument> ToHtmlDocumentAsync(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var document = await BrowsingContext.New().OpenAsync(ResponseFactory, CancellationToken.None);

            return (IHtmlDocument)document;

            void ResponseFactory(VirtualResponse virtualResponse)
            {
                virtualResponse
                    .Address(response.RequestMessage?.RequestUri)
                    .Status(response.StatusCode);

                MapHeaders(response.Headers);
                MapHeaders(response.Content.Headers);

                virtualResponse.Content(content);

                void MapHeaders(HttpHeaders headers)
                {
                    foreach (var header in headers)
                    foreach (var value in header.Value)
                        virtualResponse.Header(header.Key, value);
                }
            }
        }
    }
}
