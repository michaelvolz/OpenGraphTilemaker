using System.Threading.Tasks;

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
}
