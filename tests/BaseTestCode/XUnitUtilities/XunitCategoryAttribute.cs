using System;
using Xunit.Sdk;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace BaseTestCode.XUnitUtilities
{
    [TraitDiscoverer(CategoryDiscoverer.DiscovererTypeName, DiscovererUtil.AssemblyName)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class XunitCategoryAttribute : Attribute, ITraitAttribute
    {
        public XunitCategoryAttribute(string categoryName) => Name = categoryName;

        public string Name { get; }
    }
}