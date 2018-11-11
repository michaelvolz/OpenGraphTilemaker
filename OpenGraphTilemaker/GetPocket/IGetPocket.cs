using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.GetPocket
{
    public interface IGetPocket
    {
        Task<List<GetPocketEntry>> GetEntriesAsync(IGetPocketOptions options);
    }
}