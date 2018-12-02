using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.Globals
{
    public partial class GlobalState
    {
        [IoC]
        public class ChangeThemeColorsHandler : RequestHandler<ChangeThemeColorsRequest, GlobalState>
        {
            private readonly ILogger<ChangeThemeColorsHandler> _logger = ApplicationLogging.CreateLogger<ChangeThemeColorsHandler>();

            public ChangeThemeColorsHandler(IStore store) : base(store) { }

            public GlobalState GlobalState => Store.GetState<GlobalState>();

            public override Task<GlobalState> Handle(ChangeThemeColorsRequest req, CancellationToken token) {
                GlobalState.ThemeColor1 = req.ThemeColor1;
                GlobalState.ThemeColor2 = req.ThemeColor2;
                GlobalState.ThemeColor3 = req.ThemeColor3;

                _logger.LogWarning("{@state}", GlobalState);

                return Task.FromResult(GlobalState);
            }
        }
    }
}
