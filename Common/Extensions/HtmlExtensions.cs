using System;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace Common.Extensions
{
    public static class HtmlExtensions
    {
        public static string DeEntitize([NotNull] this string value) {
            if (value == null) throw new ArgumentNullException(nameof(value));
            
            return HtmlEntity.DeEntitize(value);
        }
    }
}