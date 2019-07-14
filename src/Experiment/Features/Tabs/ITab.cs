namespace Experiment.Features.Tabs
{
    public interface ITab
    {
        Microsoft.AspNetCore.Components.RenderFragment ChildContent { get; set; }
    }
}
