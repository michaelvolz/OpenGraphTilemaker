using System;
using System.Threading.Tasks;
using OpenGraphTilemaker.GetPocket;

namespace OpenGraphTilemaker.OpenGraph
{
    public interface ITileMakerClient
    {
        Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri, PocketEntry entry);
    }
}