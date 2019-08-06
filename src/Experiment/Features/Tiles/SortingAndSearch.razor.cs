﻿using System;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features.Tiles
{
    public class SortingAndSearchModel : BlazorComponentStateful<SortingAndSearchModel>
    {
#nullable disable
        [Parameter] protected string Class { get; set; }
        [Parameter] private Func<string, Task> OnSortProperty { get; set; }
        [Parameter] private Func<SortOrder, Task> OnSortOrder { get; set; }
        [Parameter] private Func<string, Task> OnSearch { get; set; }
        [Parameter] protected string SortProperty { get; set; }
        [Parameter] protected SortOrder SortOrder { get; set; }
        [Parameter] protected string SearchText { get; set; }
        [Parameter] protected int Count { get; set; }
#nullable enable

        private string? LastSearchText { get; set; }
        protected ElementRef SearchInput { get; set; }

        protected Task OnSortPropertyButtonClicked() => OnSortProperty(SortProperty);
        protected Task OnSortOrderButtonClicked() => OnSortOrder(SortOrder);

        protected async Task OnSearchTextChanged(UIKeyboardEventArgs args)
        {
            Logger.LogInformation($"OnSearchTextChanged() '{args.Key}'");

            if (args.Key == "Enter") await OnSearchButtonClicked();
        }

        protected Task OnSearchButtonClicked()
        {
            Logger.LogInformation("OnSearchButtonClicked()");

            if (LastSearchText == SearchText) return Task.CompletedTask;

            OnSearch(SearchText);
            LastSearchText = SearchText;

            return Task.CompletedTask;
        }

        /// <summary>
        ///     BuildRenderTree.
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            // Fires when 'enter' was pressed in the searchBox  or  searchBox -> blur
            OnSearchButtonClicked();

            base.BuildRenderTree(builder);
        }

        protected override async Task OnAfterRenderAsync() => await SearchInput.FocusAsync(ComponentContext, JSRuntime);

        public void TextInjectedFromParent(string text) =>
            Logger.LogInformation($"Received from parent control: '{text}'");
    }
}