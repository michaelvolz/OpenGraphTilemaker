using System;
using Xunit.Sdk;

namespace BaseTestCode
{
    [TraitDiscoverer(CategoryDiscoverer.DiscovererTypeName, DiscovererUtil.AssemblyName)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class TestCategoryAttribute : Attribute, ITraitAttribute
    {
        public TestCategoryAttribute(string categoryName) {
            Name = categoryName;
        }

        public string Name { get; }
    }
}