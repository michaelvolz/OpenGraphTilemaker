using System;
using JetBrains.Annotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     Decorates a class as IoC injected. No direct instantiation is necessary.
    /// </summary>
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate)]
    public class IoCAttribute : Attribute { }
}