using BlazorState;

namespace Experiment.Features.AppCode.GlobalsControl
{
    public partial class GlobalState
    {
        public class ChangeThemeColorsRequest : IAction
        {
            public string ThemeColor1 { get; init; } = string.Empty;
            public string ThemeColor2 { get; init; } = string.Empty;
            public string ThemeColor3 { get; init; } = string.Empty;
        }
    }
}
