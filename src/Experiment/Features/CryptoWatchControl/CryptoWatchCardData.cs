using System;
using JetBrains.Annotations;

namespace Experiment.Features.CryptoWatchControl
{
    public class CryptoWatchCardData
    {
        public string Name { get; [UsedImplicitly] set; } = Guid.NewGuid().ToString();
        public decimal Value { get; set; } = DateTime.Now.Millisecond;
    }
}
