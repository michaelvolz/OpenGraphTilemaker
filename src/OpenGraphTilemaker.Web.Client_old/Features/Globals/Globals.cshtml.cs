namespace OpenGraphTilemaker.Web.Client.Features.Globals
{
    public class GlobalModel : BlazorComponentStateful<GlobalModel>
    {
        public GlobalState State => Store.GetState<GlobalState>();
    }
}
