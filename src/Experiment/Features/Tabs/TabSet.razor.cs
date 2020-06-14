using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.Tabs
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Blazor rule")]
    public partial class TabSet
    {
        [Parameter]
        public RenderFragment? ChildContent { get; [UsedImplicitly] set; }

        public ITab? ActiveTab { get; private set; }

        public void AddTab(ITab tab)
        {
            if (ActiveTab == null) SetActivateTab(tab);
        }

        public void RemoveTab(ITab tab)
        {
            if (ActiveTab == tab) SetActivateTab(null);
        }

        public void SetActivateTab(ITab? tab)
        {
            if (ActiveTab != tab)
            {
                ActiveTab = tab;
                StateHasChanged();
            }
        }
    }
}
