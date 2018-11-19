using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

// ReSharper disable ClassNeverInstantiated.Global

namespace BaseTestCode
{
    public class CategoryDiscoverer : ITraitDiscoverer
    {
        internal const string DiscovererTypeName = DiscovererUtil.AssemblyName + "." + nameof(CategoryDiscoverer);

        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute) {
            var categoryName = traitAttribute.GetNamedArgument<string>("Name");

            if (!string.IsNullOrWhiteSpace(categoryName))
                yield return new KeyValuePair<string, string>("Category", categoryName);
        }
    }
}