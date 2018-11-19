using System.Net;
using System.Net.Http;
using Xunit.Abstractions;

namespace BaseTestCode
{
    public class BaseTest
    {
        protected BaseTest(ITestOutputHelper testConsole) {
            TestConsole = testConsole;
        }

        protected ITestOutputHelper TestConsole { get; }

        protected static HttpClient HttpClient(FakeHttpMessageHandler fakeHttpMessageHandler) {
            return new HttpClient(fakeHttpMessageHandler);
        }

        protected static HttpClient HttpClient(string fakeResponse, HttpStatusCode httpStatusCode = HttpStatusCode.OK) {
            var message = new HttpResponseMessage(httpStatusCode) { Content = new StringContent(fakeResponse) };
            return HttpClient(new FakeHttpMessageHandler(message));
        }
    }
}