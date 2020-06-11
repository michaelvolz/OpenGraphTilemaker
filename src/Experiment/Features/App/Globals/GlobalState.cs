namespace Experiment.Features.App.Globals
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Needed for Blazor.State")]
    public partial class GlobalState
    {
        public string ThemeColor1 { get; private set; } = string.Empty;
        public string ThemeColor2 { get; private set; } = string.Empty;
        public string ThemeColor3 { get; private set; } = string.Empty;
    }
}
