using JetBrains.Annotations;

namespace Experiment.Features.CryptoWatch
{
    public class CryptoWatchOptions
    {
        public string ApiKey { get; [UsedImplicitly] set; } = "n/a";
    }
}
