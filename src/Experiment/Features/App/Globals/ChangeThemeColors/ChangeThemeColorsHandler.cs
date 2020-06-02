﻿using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.App.Globals
{
    public partial class GlobalState
    {
        [IoC]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public class ChangeThemeColorsHandler : ActionHandler<ChangeThemeColorsRequest>
        {
            private readonly ILogger<ChangeThemeColorsHandler> _logger = ApplicationLogging.CreateLogger<ChangeThemeColorsHandler>();

            public ChangeThemeColorsHandler(IStore store) : base(store) { }

            public GlobalState GlobalState => Store.GetState<GlobalState>();

            public override Task<Unit> Handle(ChangeThemeColorsRequest req, CancellationToken token)
            {
                GlobalState.ThemeColor1 = req.ThemeColor1;
                GlobalState.ThemeColor2 = req.ThemeColor2;
                GlobalState.ThemeColor3 = req.ThemeColor3;

                _logger.LogWarning("{@state}", GlobalState);

                return Unit.Task;
            }
        }
    }
}