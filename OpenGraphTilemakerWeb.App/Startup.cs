using BlazorState;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenGraphTilemakerWeb.App.Services;

namespace OpenGraphTilemakerWeb.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorState();

            // Since Blazor is running on the server, we can use an application service
            // to read the forecast data.
            services.AddSingleton<WeatherForecastService>();
        }

        // ReSharper disable All
        public void Configure(IBlazorApplicationBuilder app) => app.AddComponent<App>("app");
        // ReSharper restore All

    }
}