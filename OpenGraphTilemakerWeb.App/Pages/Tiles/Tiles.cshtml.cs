using System;
using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class TilesModel : BlazorComponent
    {
        public TilesState TilesState => Store.GetState<TilesState>();

        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IStore Store { get; set; }

        protected override void OnInit()
        {
            var request = new InitializeTilesRequest();
            var send = Mediator.Send(request);

            if (send.IsFaulted)
                throw new InvalidOperationException(send.Exception?.Message);
        }

        protected void OnSortPropertyButtonClick()
        {
            var sortProperty = TilesState.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.SourcePublishTime);


            var request = new SortTilesRequest {SortProperty = sortProperty};
            var send = Mediator.Send(request);

            if (send.IsFaulted)
                throw new InvalidOperationException(send.Exception?.Message);

            StateHasChanged();
        }

        protected void OnSortOrderButtonClick()
        {
            var sortOrder = TilesState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

            var request = new SortTilesRequest {SortOrder = sortOrder};
            var send = Mediator.Send(request);

            if (send.IsFaulted)
                throw new InvalidOperationException(send.Exception?.Message);

            StateHasChanged();
        }
    }
}