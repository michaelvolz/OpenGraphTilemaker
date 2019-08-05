using System;
using System.Reflection;
using BlazorState;
using Common;
using Common.Extensions;
using Experiment.Features.CryptoWatch;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Experiment
{
    public partial class Startup
    {
        private static class Extensions
        {
            public static void BlazorState(IServiceCollection services)
            {
                services.AddBlazorState
                (
                    options => options.Assemblies =
                        new[]
                        {
                            typeof(Startup).GetTypeInfo().Assembly
                        }
                );

                services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
                services.Scan
                (
                    typeSourceSelector => typeSourceSelector
                        .FromAssemblyOf<Startup>()
                        .AddClasses()
                        .AsSelf()
                        .WithScopedLifetime()
                );
            }

            public static void VerifyCryptoWatchApiKey(ILogger<Startup> logger)
            {
                //var cryptoWatchOptions = ServiceLocator.Current.GetInstance<IOptions<CryptoWatchOptions>>();
                //if (cryptoWatchOptions == null || cryptoWatchOptions.Value.ApiKey == "n/a")
                //    throw new InvalidOperationException("CryptoWatchOptions ApiKey not configured!");

                //logger.LogInformation("CryptoWatch ApiKey: " + cryptoWatchOptions.Value.ApiKey.TruncateAtWord(5, "..."));
            }

            public static ILogger<Startup> VerifyLogger()
            {
                var logger = ServiceLocator.Current.GetInstance<ILogger<Startup>>();

                if (logger == null) throw new InvalidOperationException("ILogger<> not found!");

                return logger;
            }
        }
    }
}