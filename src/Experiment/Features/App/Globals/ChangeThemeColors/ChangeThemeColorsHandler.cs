using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
        public class ChangeThemeColorsHandler : ActionHandler<ChangeThemeColorsRequest>
        {
            private readonly ILogger<ChangeThemeColorsHandler> _logger = ApplicationLogging.CreateLogger<ChangeThemeColorsHandler>();

            public ChangeThemeColorsHandler(IStore store) : base(store) { }

            private GlobalState GlobalState => Store.GetState<GlobalState>();

            public override Task<Unit> Handle(ChangeThemeColorsRequest aAction, CancellationToken aCancellationToken)
            {
                Guard.Against.Null(aAction, nameof(aAction));

                GlobalState.ThemeColor1 = aAction.ThemeColor1;
                GlobalState.ThemeColor2 = aAction.ThemeColor2;
                GlobalState.ThemeColor3 = aAction.ThemeColor3;

                _logger.LogWarning("{@state}", GlobalState);

                return Unit.Task;
            }
        }
    }
}
