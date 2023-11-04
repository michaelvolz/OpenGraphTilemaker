using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;

namespace Experiment
{
    public partial class Program
    {
        private static class ApplicationSettings
        {
            // ReSharper disable once InconsistentNaming
            private const string AppSettingsJSON = "appsettings.json";

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

                return File.Exists(Path.Combine(currentDirectory, AppSettingsJSON))
                    ? currentDirectory
                    : FixPathForTestingEnvironment(currentDirectory);

                static string FixPathForTestingEnvironment(string currentDirectory)
                {
                    return Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "..", "..", ".."));
                }
            }
        }
    }
}
