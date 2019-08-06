using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public static class ElementRefExtensions
    {
        public static async Task FocusAsync(this ElementRef elementRef) => await JSInteropHelpers.FocusAsync(elementRef);
    }
}
