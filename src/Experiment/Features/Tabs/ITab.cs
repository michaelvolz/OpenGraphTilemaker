// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Components;

namespace Experiment.Features.Tabs
{
    public interface ITab
    {
        RenderFragment? ChildContent { get; set; }
    }
}