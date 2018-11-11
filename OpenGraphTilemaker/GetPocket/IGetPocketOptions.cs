using System;

namespace OpenGraphTilemaker.GetPocket
{
    public interface IGetPocketOptions
    {
        Uri Uri { get; set; }
        TimeSpan CachingTimeSpan { get; set; }
    }
}