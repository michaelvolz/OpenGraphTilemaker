using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class IndexModel : BlazorComponent
    {
        [Inject] protected AppState AppState { get; set; }

        protected override async Task OnInitAsync()
        {
            AppState.OnTilesChanged += StateHasChanged;

            await AppState.Initialize();
        }
    }
}