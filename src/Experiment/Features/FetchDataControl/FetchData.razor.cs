using System;
using System.Threading.Tasks;
using Experiment.Features.AppCode;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment.Features.FetchDataControl
{
    public partial class FetchData
    {
        [Inject] private WeatherForecastService ForecastService { get; [UsedImplicitly] set; } = null!;

        private WeatherForecast[] Forecasts { get; set; } = Array.Empty<WeatherForecast>();

        protected override async Task OnInitializedAsync() => Forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
    }
}
