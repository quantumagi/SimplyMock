using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MoqExt
{
    public static class IServiceCollectionExt
    {
        /// <summary>
        /// Allows a specific constructor to be used when creating a service.
        /// </summary>
        public static IServiceCollection AddSingleton<T>(this IServiceCollection serviceCollection, ConstructorInfo constructorInfo) where T : class
        {
            serviceCollection.AddSingleton(typeof(T), (s) => constructorInfo.Invoke(MockingContext.GetConstructorArguments(s, constructorInfo)));

            return serviceCollection;
        }
    }
}
