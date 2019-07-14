using MediatR;

namespace Experiment.Features.Globals
{
    public class ChangeThemeColorsRequest : IRequest<GlobalState>
    {
        public string ThemeColor1 { get; set; }
        public string ThemeColor2 { get; set; }
        public string ThemeColor3 { get; set; }
    }
}
