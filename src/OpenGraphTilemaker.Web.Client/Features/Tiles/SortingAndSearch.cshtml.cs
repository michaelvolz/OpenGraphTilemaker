using System;
using System.Threading.Tasks;
using Common;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class SortingAndSearchModel : BlazorComponentStateful<SortingAndSearchModel>
    {
        [Parameter] private Action<string> OnSortProperty { get; set; }
        [Parameter] private Action<SortOrder> OnSortOrder { get; set; }
        [Parameter] private Action<string> OnSearch { get; set; }

        [Parameter] protected string SortProperty { get; set; }
        [Parameter] protected SortOrder SortOrder { get; set; }
        [Parameter] protected string SearchText { get; set; }
        [Parameter] protected int Count { get; set; }

        private string LastSearchText { get; set; }
        protected ElementRef SearchInput { get; set; }

        protected void OnSortPropertyButtonClicked() => OnSortProperty(SortProperty);
        protected void OnSortOrderButtonClicked() => OnSortOrder(SortOrder);

        protected void OnSearchButtonClicked() {
            if (LastSearchText == SearchText) return;

            OnSearch(SearchText);
            LastSearchText = SearchText;
        }

        /// <summary>
        ///     BuildRenderTree.
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            // Fires when 'enter' was pressed in the searchBox  or  searchBox -> blur
            OnSearchButtonClicked();

            base.BuildRenderTree(builder);
        }

        protected override async Task OnAfterRenderAsync() {
            await base.OnAfterRenderAsync();

            await SearchInput.FocusAsync();
        }

        [UsedImplicitly]
        public void TextInjectedFromParent(string text) {
            Log.LogInformation($"Received from parent control: '{text}'");
        }
    }
}
