namespace Experiment.Features.AppCode.NavMenuControl
{
    public partial class NavMenu
    {
        private string? NavMenuCSS { get; set; }

        private void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;
    }
}
