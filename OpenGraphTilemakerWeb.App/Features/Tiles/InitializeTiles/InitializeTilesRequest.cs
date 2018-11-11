using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemakerWeb.App.Features.Tiles
{
    public class InitializeTilesRequest : IRequest<TilesState> { }
}