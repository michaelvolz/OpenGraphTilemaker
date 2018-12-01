using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.Global
{
    public class ChangeThemeColorsRequest : IRequest<GlobalState>
    {
        public string ThemeColor1 { get; set; }
        public string ThemeColor2 { get; set; }
        public string ThemeColor3 { get; set; }
    }
}
