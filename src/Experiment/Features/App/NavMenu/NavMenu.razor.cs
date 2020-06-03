namespace Experiment.Features.App.NavMenu
{
    public class NavMenuModel : BlazorComponentStateful<NavMenuModel>
    {
        protected string? NavMenuCSS { get; private set; }

        protected void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;
    }
}