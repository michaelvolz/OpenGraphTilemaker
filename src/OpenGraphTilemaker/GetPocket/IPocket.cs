using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.GetPocket
{
    public interface IPocket
    {
        Task<List<PocketEntry>> GetEntriesAsync(IPocketOptions options);
    }
}