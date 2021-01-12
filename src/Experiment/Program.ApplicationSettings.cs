using System;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Experiment
{
    public partial class Program
    {
        [UsedImplicitly]
        private class ApplicationSettings
        {
            private const string AppSettingsJSON = "appsettings.json";
            private const string FixedPathExtensionForTesting = @"..\..\..\..\..\..\src\Experiment";

            public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                .SetBasePath(LocateAppSettings())
                .AddJsonFile(AppSettingsJSON, false, true)
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .AddUserSecrets("15ea9d85-0125-491c-bbf6-6e4acc1703a6")
                .AddEnvironmentVariables()
                .Build();

            private static string LocateAppSettings()
            {
                var currentDirectory = Directory.GetCurrentDirectory();

                return File.Exists($@"{currentDirectory}\{AppSettingsJSON}")
                    ? currentDirectory
                    : currentDirectory + FixedPathExtensionForTesting;
            }
        }
    }
}
