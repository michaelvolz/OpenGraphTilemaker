namespace Experiment.Features.AppCode.GlobalsControl
{
    public partial class Globals
    {
        public GlobalState State => Store.GetState<GlobalState>();
    }
}
