using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class IndexModel : BlazorComponent
    {
        [Inject] protected AppState AppState { get; set; }

        protected override void OnInit()
        {
            AppState.OnSort += StateHasChanged;
        }

        protected void OnSortPropertyButtonClick()
        {
            AppState.SortProperty = AppState.SortProperty != "Title" ? "Title" : "PubDate";
            AppState.Sort();
        }
        
        protected void OnSortOrderButtonClick()
        {
            AppState.SortOrder = AppState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            AppState.Sort();
        }

    }
}