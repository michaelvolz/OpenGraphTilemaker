using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class ServiceLocator
    {
        private static ServiceProvider? _globalServiceProvider;
        private readonly ServiceProvider _localServiceProvider;

        private ServiceLocator(ServiceProvider currentServiceProvider) =>
            _localServiceProvider = Guard.Against.Null(currentServiceProvider, nameof(currentServiceProvider));

        public static ServiceLocator Current =>
            new ServiceLocator(_globalServiceProvider ?? throw new InvalidOperationException("GlobalServiceProvider uninitialized! use 'SetServiceProvider' first!"));

        public static void SetServiceProvider(ServiceProvider serviceProvider) => _globalServiceProvider = serviceProvider;

        public object GetInstance(Type serviceType) => _localServiceProvider.GetService(serviceType);

        public TService GetInstance<TService>() => _localServiceProvider.GetService<TService>();
    }
}