using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Xunit;

namespace BaseTestCode.XUnitUtilities
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class IgnoreOnAppVeyorLinuxFactAttribute : FactAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IgnoreOnAppVeyorLinuxFactAttribute" /> class.
        ///     Ignore on Linux when run via AppVeyor
        /// </summary>
        public IgnoreOnAppVeyorLinuxFactAttribute()
        {
            if (IsLinux() && IsAppVeyor()) Skip = "Ignore on Linux when run via AppVeyor";
        }

        private static bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        private static bool IsAppVeyor() => Environment.GetEnvironmentVariable("APPVEYOR") != null;
    }
}