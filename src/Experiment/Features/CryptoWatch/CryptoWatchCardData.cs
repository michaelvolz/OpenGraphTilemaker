using System;

namespace Experiment.Features.CryptoWatch
{
    public class CryptoWatchCardData
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public decimal Value { get; set; } = DateTime.Now.Millisecond;
    }
}
