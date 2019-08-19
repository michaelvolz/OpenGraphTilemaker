namespace Experiment.Features.App.Globals
{
    public class GlobalModel : BlazorComponentStateful<GlobalModel>
    {
        public GlobalState State => Store.GetState<GlobalState>();
    }
}