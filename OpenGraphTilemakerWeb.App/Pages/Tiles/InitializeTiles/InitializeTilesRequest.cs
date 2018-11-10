using MediatR;

// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class InitializeTilesRequest : IRequest<TilesState>
    {
    }
}