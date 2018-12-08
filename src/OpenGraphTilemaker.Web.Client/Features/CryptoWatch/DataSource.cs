using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace OpenGraphTilemaker.Web.Client.Features.CryptoWatch
{
    public class DataSource
    {
        public Action<int> OnUpdate { get; set; }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public async Task GoAsync() {
            await Task.FromResult(0);

            var logger = ApplicationLogging.CreateLogger<DataSource>();

            logger.LogDebug("DataSource start");
            for (var i = 0; i < 10000; i++) {
                // logger.LogDebug(i.ToString());
                OnUpdate(i);

                var x = 0;
                for (var j = 0; j < 25000; j++) x += 1;

                // Thread.Sleep(1);
                // await Task.Delay(1);
            }

            logger.LogDebug("DataSource end");
        }
    }
}
