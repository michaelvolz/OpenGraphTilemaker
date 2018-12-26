using System.IO;
using System.Reflection;

namespace Common.Extensions
{
    public static class AssemblyExtensions
    {
        public static readonly string AssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
