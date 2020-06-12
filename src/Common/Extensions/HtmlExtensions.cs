using Ardalis.GuardClauses;
using HtmlAgilityPack;

namespace Common.Extensions
{
    public static class HtmlExtensions
    {
        public static string DeEntitize(this string str)
        {
            Guard.Against.NullOrWhiteSpace(() => str);

            return HtmlEntity.DeEntitize(str);
        }
    }
}
