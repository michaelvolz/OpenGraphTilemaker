using System;
using System.Threading.Tasks;
using Domain.OpenGraphTilemaker.GetPocket;

namespace Domain.OpenGraphTilemaker.OpenGraph
{
    public interface ITileMakerClient
    {
        Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri, PocketEntry entry);
    }
}
