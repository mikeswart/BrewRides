using System;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalBreweryBikeClub.Internal
{
    internal static class ServiceScopeFactoryExtensions
    {
        public static IDisposable CreateDatabaseContextScope<T>(this IServiceScopeFactory scopeFactory, out T scopedInstance)
        {
            var scope = scopeFactory.CreateScope();
            scopedInstance = scope.ServiceProvider.GetService<T>();

            return System.Reactive.Disposables.Disposable.Create(() =>
            {
                scope.Dispose();
            });
        }
    }
}