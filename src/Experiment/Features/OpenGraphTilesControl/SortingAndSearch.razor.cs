﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Experiment.Features.AppCode;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.OpenGraphTilesControl
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Utility class")]
    public partial class SortingAndSearch
    {
        [Parameter] public Func<string, Task>? OnSortProperty { get; [UsedImplicitly] set; }
        [Parameter] public Func<SortOrder, Task>? OnSortOrder { get; [UsedImplicitly] set; }
        [Parameter] public Func<string, Task>? OnSearch { get; [UsedImplicitly] set; }

        [Parameter] public string Class { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public string SortProperty { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public SortOrder SortOrder { get; [UsedImplicitly] set; }
        [Parameter] public string SearchText { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public int Count { get; [UsedImplicitly] set; }

        public ElementReference SearchInput { get; [UsedImplicitly] set; }

        protected Task OnSortPropertyButtonClicked()
        {
            Guard.Against.Null(OnSortProperty, nameof(OnSortProperty));
            Guard.Against.NullOrWhiteSpace(() => SortProperty);

            return OnSortProperty!(SortProperty!);
        }

        protected Task OnSortOrderButtonClicked()
        {
            Guard.Against.Null(OnSortOrder, nameof(OnSortOrder));
            Guard.Against.Default(() => SortOrder);

            return OnSortOrder!(SortOrder);
        }

        protected async Task OnSearchTextChanged(KeyboardEventArgs args)
        {
            Guard.Against.Null(args, nameof(args));

            Logger.LogInformation("OnSearchTextChanged() '{Key}'", args.Key);

            if (args.Key == "Enter") await OnSearchButtonClicked();
        }

        protected Task OnSearchButtonClicked()
        {
            Guard.Against.Null(OnSearch, nameof(OnSearch));
            Guard.Against.Null(SearchText, nameof(SearchText));

            OnSearch!(SearchText);

            return Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) => await SearchInput.FocusAsync(JSRuntime);
    }
}
