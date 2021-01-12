using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CryptoWatch
{
    public class FakeDataPump
    {
        private readonly Random _random;

        public FakeDataPump() => _random = new Random();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "Can be insecure")]
        public async IAsyncEnumerable<CryptoEntity> DataStreamAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var entity = new CryptoEntity { Name = "Bitcoin", Value = _random.Next(10000), ValueCurrency = "USD" };
                yield return entity;

                await Task.Delay(_random.Next(100), CancellationToken.None);
            }
        }
    }
}
