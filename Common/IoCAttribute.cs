using System;
using JetBrains.Annotations;

namespace Common
{
    /// <summary>
    ///     Decorates a class as IoC injected. No direct instantiation is necessary. Check
    ///     <see cref="M:UnityConfig.RegisterTypes(IUnityContainer container)" />
    /// </summary>
    [MeansImplicitUse]
    public class IoCAttribute : Attribute {}
}
