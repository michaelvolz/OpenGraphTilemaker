using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.App.Visibility
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Nested file")]
    public partial class Visibility
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public bool Show { get; set; }
    }
}
