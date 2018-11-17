using System;
using JetBrains.Annotations;

namespace Common
{
    /// <inheritdoc />
    /// <summary>
    ///     Decorates a class as IoC injected. No direct instantiation is necessary.
    /// </summary>
    [MeansImplicitUse]
    public class IoCAttribute : Attribute { }
}