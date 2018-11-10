using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class PingPongTest
    {
        public class Ping : IRequest<string> { }

        public class PingHandler : IRequestHandler<Ping, string> {
            public async Task<string> Handle(Ping request, CancellationToken cancellationToken) {
                return await Task.FromResult("Pong");
            }
        }
    }
}
