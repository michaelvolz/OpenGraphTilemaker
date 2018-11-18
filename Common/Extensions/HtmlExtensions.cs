using Ardalis.GuardClauses;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace Common.Extensions
{
    public static class HtmlExtensions
    {
        public static string DeEntitize([NotNull] this string value) {
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            return HtmlEntity.DeEntitize(value);
        }
    }
}