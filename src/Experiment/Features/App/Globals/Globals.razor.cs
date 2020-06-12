namespace Experiment.Features.App.Globals
{
    public partial class Globals
    {
        public GlobalState State => Store.GetState<GlobalState>();
    }
}
