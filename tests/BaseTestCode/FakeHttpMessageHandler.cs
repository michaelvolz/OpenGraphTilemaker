using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BaseTestCode
{
    public class FakeHttpMessageHandler : DelegatingHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public FakeHttpMessageHandler(HttpResponseMessage fakeResponse) => _fakeResponse = fakeResponse;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(_fakeResponse);
    }
}
