using HtmlAgilityPack;

namespace Common.Extensions
{
    public static class HtmlExtensions
    {
        public static string DeEntitize(this string value) => HtmlEntity.DeEntitize(value);
    }
}