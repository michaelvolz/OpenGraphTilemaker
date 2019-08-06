using System;
using Xunit;

namespace BaseTestCode.XUnitUtilities
{
    public sealed class IgnoreOnAzurePipelinesFactAttribute : FactAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IgnoreOnAzurePipelinesFactAttribute" /> class.
        ///     Ignore on Linux when run via AppVeyor
        /// </summary>
        public IgnoreOnAzurePipelinesFactAttribute()
        {
            if (IsAzurePipelines()) Skip = "Ignore when run via AzurePipelines";
        }

        private static bool IsAzurePipelines() => Environment.GetEnvironmentVariable("TF_BUILD") != null;
    }
}