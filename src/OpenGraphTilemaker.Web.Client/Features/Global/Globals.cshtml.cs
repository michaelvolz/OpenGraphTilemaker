﻿namespace OpenGraphTilemaker.Web.Client.Features.Global
{
    public class GlobalModel : BlazorComponentStateful<GlobalModel>
    {
        public GlobalState State => Store.GetState<GlobalState>();
    }
}
