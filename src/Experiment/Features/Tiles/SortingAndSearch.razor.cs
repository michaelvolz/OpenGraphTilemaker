using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMethodReturnValue.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features.Tiles
{
    public class SortingAndSearchModel : BlazorComponentStateful<SortingAndSearchModel>
    {
        [Parameter] public Func<string, Task>? OnSortProperty { get; set; }
        [Parameter] public Func<SortOrder, Task>? OnSortOrder { get; set; }
        [Parameter] public Func<string, Task>? OnSearch { get; set; }

        [Parameter] public string Class { get; set; } = string.Empty;
        [Parameter] public string SortProperty { get; set; } = string.Empty;
        [Parameter] public SortOrder SortOrder { get; set; }
        [Parameter] public string SearchText { get; set; } = string.Empty;
        [Parameter] public int Count { get; set; }

        public ElementReference SearchInput { get; set; }

        protected Task OnSortPropertyButtonClicked()
        {
            Guard.Against.Null(() => OnSortProperty);
            Guard.Against.NullOrWhiteSpace(() => SortProperty);

            return OnSortProperty!(SortProperty!);
        }

        protected Task OnSortOrderButtonClicked()
        {
            Guard.Against.Null(() => OnSortOrder);
            Guard.Against.Default(() => SortOrder);

            return OnSortOrder!(SortOrder);
        }

        protected async Task OnSearchTextChanged(UIKeyboardEventArgs args)
        {
            Logger.LogInformation("OnSearchTextChanged() '{Key}'", args.Key);

            if (args.Key == "Enter") await OnSearchButtonClicked();
        }

        protected Task OnSearchButtonClicked()
        {
            Guard.Against.Null(() => OnSearch);
            Guard.Against.Null(() => SearchText);

            OnSearch!(SearchText);

            return Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync() => await SearchInput.FocusAsync(ComponentContext, JSRuntime);

        public void TextInjectedFromParent(string text) =>
            Logger.LogInformation("Received from parent control: '{Text}'", text);
    }
}