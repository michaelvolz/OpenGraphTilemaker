using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.App.Visibility
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Blazor rule")]
    public partial class Visibility
    {
        [Parameter] public RenderFragment? ChildContent { get; [UsedImplicitly] set; }
        [Parameter] public bool Show { get; [UsedImplicitly] set; }
    }
}
