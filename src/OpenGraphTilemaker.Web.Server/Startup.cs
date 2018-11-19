using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace OpenGraphTilemaker.Web.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) {
            services.AddServerSideBlazor<Client.Startup>();

            services.AddResponseCompression(options => {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseResponseCompression();

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseServerSideBlazor<Client.Startup>();
        }
    }
}