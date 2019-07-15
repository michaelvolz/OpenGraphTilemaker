using JetBrains.Annotations;

namespace Experiment.Features
{
    public class Window
    {
        public int Height { get; [UsedImplicitly] set; }
        public int Width { get; [UsedImplicitly] set; }
    }
}
