using System;
using CSF.DecoratorBuilder;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services to the service collection so that decorator-builder services are available from dependency injection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method adds implementations of the following interfaces to the service collection:
        /// </para>
        /// <list type="bullet">
        /// <item><description><see cref="IGetsDecoratedService"/></description></item>
        /// <item><description><see cref="IGetsDecoratedServiceFromResolutionInfo"/></description></item>
        /// <item><description><see cref="IGetsSingleObjectFromResolutionInfo"/></description></item>
        /// </list>
        /// <para>
        /// This method must be used in order to activate the decorator builder functionality within
        /// dependency injection for your application.
        /// </para>
        /// </remarks>
        /// <param name="services">A service collection.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddDecoratorBuilder(this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddTransient<IGetsDecoratedService, DecoratedServiceFactory>();
            services.AddTransient<IGetsDecoratedServiceFromResolutionInfo, ServiceFromResolutionInfoResolver>();
            services.AddTransient<IGetsSingleObjectFromResolutionInfo, DependencyInjectionObjectResolver>();
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a transient lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service collection.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddTransientDecorator<TService>(this IServiceCollection services,
                                                           Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc,
                                                           params ITypedResolvable[] globalParams)
            where TService : class
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddTransient<TService>(GetDecoratedServiceFactory<TService>(customizationFunc, globalParams));
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a transient lifetime.
        /// </summary>
        /// <param name="services">A service collection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddTransientDecorator(this IServiceCollection services,
                                                 Type serviceType,
                                                 Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc,
                                                 params ITypedResolvable[] globalParams)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddTransient(serviceType, GetDecoratedServiceFactory(serviceType, customizationFunc, globalParams));
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a scoped lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service collection.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddScopedDecorator<TService>(this IServiceCollection services,
                                                        Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc,
                                                        params ITypedResolvable[] globalParams)
            where TService : class
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<TService>(GetDecoratedServiceFactory<TService>(customizationFunc, globalParams));
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a scoped lifetime.
        /// </summary>
        /// <param name="services">A service collection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddScopedDecorator(this IServiceCollection services,
                                              Type serviceType,
                                              Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc,
                                              params ITypedResolvable[] globalParams)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped(serviceType, GetDecoratedServiceFactory(serviceType, customizationFunc, globalParams));
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a singleton lifetime.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service collection.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddSingletonDecorator<TService>(this IServiceCollection services,
                                                           Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc,
                                                           params ITypedResolvable[] globalParams)
            where TService : class
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<TService>(GetDecoratedServiceFactory<TService>(customizationFunc, globalParams));
        }

        /// <summary>
        /// Adds logic for a service, resolved using the decorator pattern, to the service collection.
        /// This method adds the service with a singleton lifetime.
        /// </summary>
        /// <param name="services">A service collection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="customizationFunc">A function/lambda which specifies how the service is to be built, via the decorator pattern.</param>
        /// <param name="globalParams">An optional collection of parameters which will be globally applied to every resolution operation.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <see langword="null" />.</exception>
        public static void AddSingletonDecorator(this IServiceCollection services,
                                                 Type serviceType,
                                                 Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc,
                                                 params ITypedResolvable[] globalParams)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(serviceType, GetDecoratedServiceFactory(serviceType, customizationFunc, globalParams));
        }

        static Func<IServiceProvider,T> GetDecoratedServiceFactory<T>(Func<ICreatesDecorator<T>,ICustomizesDecorator<T>> customizationFunc,
                                                                      ITypedResolvable[] globalParams)
            where T : class
        {
            return s =>
            {
                var builder = s.GetRequiredService<IGetsDecoratedService>();
                return builder.GetDecoratedService<T>(customizationFunc, globalParams);
            };
        }

        static Func<IServiceProvider,object> GetDecoratedServiceFactory(Type serviceType,
                                                                        Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc,
                                                                        ITypedResolvable[] globalParams)
        {
            return s =>
            {
                var builder = s.GetRequiredService<IGetsDecoratedService>();
                return builder.GetDecoratedService(serviceType, customizationFunc, globalParams);
            };
        }
    }
}