using System;
using JetBrains.Annotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     Decorates a class as EventMethod for Blazor views. No direct instantiation is necessary.
    /// </summary>
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Enum |
                    AttributeTargets.Interface |
                    AttributeTargets.Delegate)]
    public class BlazorEventAttribute : Attribute { }
}