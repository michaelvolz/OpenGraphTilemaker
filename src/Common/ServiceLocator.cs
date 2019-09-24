using System;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

namespace Common
{
    public class ServiceLocator
    {
        private static ServiceProvider? _serviceProvider;
        private readonly ServiceProvider _currentServiceProvider;

        private ServiceLocator(ServiceProvider currentServiceProvider) =>
            _currentServiceProvider = Guard.Against.Null(() => currentServiceProvider);

        public static ServiceLocator Current =>
            new ServiceLocator(_serviceProvider ?? throw new InvalidOperationException("ServiceProvider uninitialized! use 'SetServiceProvider' first!"));

        public static void SetServiceProvider(ServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public object GetInstance(Type serviceType) => _currentServiceProvider.GetService(serviceType);

        public TService GetInstance<TService>() => _currentServiceProvider.GetService<TService>();
    }
}