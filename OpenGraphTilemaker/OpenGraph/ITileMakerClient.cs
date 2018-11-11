using System;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.OpenGraph
{
    public interface ITileMakerClient
    {
        Task<OpenGraphMetadata> OpenGraphMetadataAsync(Uri uri);
    }
}