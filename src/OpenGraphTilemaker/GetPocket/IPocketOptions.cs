using System;
using JetBrains.Annotations;

namespace OpenGraphTilemaker.GetPocket
{
    public interface IPocketOptions
    {
        Uri? Uri { get; [UsedImplicitly] set; }
        TimeSpan CachingTimeSpan { get; [UsedImplicitly] set; }
        TimeSpan TimeOutTimeSpan { get; [UsedImplicitly] set; }
    }
}