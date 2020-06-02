using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.Tabs
{
    public interface ITab
    {
        RenderFragment? ChildContent { get; [UsedImplicitly] set; }
    }
}