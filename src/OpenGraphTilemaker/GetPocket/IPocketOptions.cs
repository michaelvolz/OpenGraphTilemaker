﻿using System;

namespace OpenGraphTilemaker.GetPocket
{
    public interface IPocketOptions
    {
        Uri Uri { get; set; }
        TimeSpan CachingTimeSpan { get; set; }
        TimeSpan TimeOutTimeSpan { get; set; }
    }
}
