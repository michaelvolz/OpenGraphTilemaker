namespace Experiment.Features.App.NavMenu
{
    public partial class NavMenu
    {
        private string? NavMenuCSS { get; set; }

        private void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;
    }
}
