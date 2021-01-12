using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Experiment.Features.CryptoWatchControl
{
    public partial class CryptoWatch
    {
        private CryptoWatchCardData Card1 { get; [UsedImplicitly] set; } = new CryptoWatchCardData();
        private CryptoWatchCardData Card2 { get; [UsedImplicitly] set; } = new CryptoWatchCardData();
        private CryptoWatchCardData Card3 { get; [UsedImplicitly] set; } = new CryptoWatchCardData();
        private CryptoWatchCardData Card4 { get; [UsedImplicitly] set; } = new CryptoWatchCardData();

        protected override void OnInitialized() => Task.Run(BackgroundTask);

        private async Task BackgroundTask()
        {
            var dataSource = new DataSource();
            dataSource.OnUpdate += DataSourceOnUpdate;
            await dataSource.GoAsync();
            dataSource.OnUpdate -= DataSourceOnUpdate;
        }

        private void DataSourceOnUpdate(int v)
        {
            Card1.Value = v;
            StateHasChanged();
        }
    }
}
