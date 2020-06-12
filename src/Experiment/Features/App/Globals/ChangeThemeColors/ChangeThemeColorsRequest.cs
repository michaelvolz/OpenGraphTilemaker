using BlazorState;

namespace Experiment.Features.App.Globals
{
    public partial class GlobalState
    {
        public class ChangeThemeColorsRequest : IAction
        {
            public string ThemeColor1 { get; set; } = string.Empty;
            public string ThemeColor2 { get; set; } = string.Empty;
            public string ThemeColor3 { get; set; } = string.Empty;
        }
    }
}
