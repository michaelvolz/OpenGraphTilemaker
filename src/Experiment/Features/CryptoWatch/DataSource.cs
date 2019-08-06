﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.CryptoWatch
{
    public class DataSource
    {
#nullable disable
        public Action<int> OnUpdate { get; set; }
#nullable enable

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public async Task GoAsync() {
            var logger = ApplicationLogging.CreateLogger<DataSource>();

            logger.LogDebug("DataSource start");
            for (var i = 0; i < 10000; i++) {
                logger.LogDebug(i.ToString());
                OnUpdate?.Invoke(i);

                var x = 0;
                for (var j = 0; j < 25000; j++) x += 1;

                //Thread.Sleep(1);
                //await Task.Delay(1);
            }

            logger.LogDebug("DataSource end");

            await Task.FromResult(0);
        }
    }
}