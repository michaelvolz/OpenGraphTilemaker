using System.Threading;
using System.Threading.Tasks;
using MediatR;

public class Ping : IRequest<string> { }

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> Handle(Ping request, CancellationToken cancellationToken) => Task.FromResult("Pong");
}
