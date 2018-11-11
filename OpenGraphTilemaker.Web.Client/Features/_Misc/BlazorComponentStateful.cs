using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class BlazorComponentStateful : BlazorComponent, IBlazorStateComponent
    {
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IStore Store { get; set; }

        protected void Request<T>(IRequest<T> request) {
            var send = Mediator.Send(request);

            if (send.IsFaulted)
                throw new MediatRAccessException(send.Exception?.Message);
        }
    }
}