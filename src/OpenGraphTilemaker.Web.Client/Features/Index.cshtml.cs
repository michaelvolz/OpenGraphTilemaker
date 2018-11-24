using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful
    {
        protected JSInteropHelpers.JSState State { get; private set; }

        protected override async Task OnParametersSetAsync() {
            State = await JSInteropHelpers.OnParametersSet();
            State.OnWindowResized += StateHasChanged;

            await base.OnParametersSetAsync();
        }
    }
}
