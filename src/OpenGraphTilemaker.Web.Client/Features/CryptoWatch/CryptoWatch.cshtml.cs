using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace OpenGraphTilemaker.Web.Client.Features.CryptoWatch
{
    public class CryptoWatchModel : BlazorComponentStateful<CryptoWatchModel>
    {
        public CryptoWatchCardData Card1 { get; set; } = new CryptoWatchCardData();
        public CryptoWatchCardData Card2 { get; set; } = new CryptoWatchCardData();
        public CryptoWatchCardData Card3 { get; set; } = new CryptoWatchCardData();
        public CryptoWatchCardData Card4 { get; set; } = new CryptoWatchCardData();

        protected override void OnInit() => Task.Run(BackgroundTask);

        private async Task BackgroundTask() {
            var dataSource = new DataSource();
            dataSource.OnUpdate += DataSourceOnUpdate;
            await dataSource.GoAsync();
            dataSource.OnUpdate -= DataSourceOnUpdate;
        }

        private void DataSourceOnUpdate(int v) {
            Card1.Value = v;
            StateHasChanged();
        }
    }


    public class DataSource
    {
        public Action<int> OnUpdate;

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public async Task GoAsync() {
            var logger = ApplicationLogging.CreateLogger<DataSource>();

            logger.LogDebug("DataSource start");
            for (var i = 0; i < 10000; i++) {
                //logger.LogDebug(i.ToString());
                OnUpdate(i);

                int x = 0;
                for (int j = 0; j < 25000; j++) {
                    x += 1;
                }

                //Thread.Sleep(1);
                //await Task.Delay(1);
            }
            logger.LogDebug("DataSource end");
        }
    }
}
