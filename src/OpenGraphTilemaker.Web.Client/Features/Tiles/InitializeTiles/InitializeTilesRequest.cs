using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class InitializeTilesRequest : IRequest<TilesState> { }
}
