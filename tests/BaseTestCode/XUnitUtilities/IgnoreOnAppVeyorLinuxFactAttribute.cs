using System;
using System.Runtime.InteropServices;
using Xunit;

// ReSharper disable UnusedMember.Global

namespace BaseTestCode.XUnitUtilities
{
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