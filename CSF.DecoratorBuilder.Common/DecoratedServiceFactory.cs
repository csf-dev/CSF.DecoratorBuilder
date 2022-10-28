using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsDecoratedService"/> which creates an appropriate
    /// builder, applies customisations and returns the resolved service, created using the
    /// decorator pattern.
    /// </summary>
    public class DecoratedServiceFactory : IGetsDecoratedService
    {
        readonly IGetsDecoratedServiceFromResolutionInfo resolver;

        /// <inheritdoc/>
        public TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>, ICustomizesDecorator<TService>> customizationFunc,
                                                      params ITypedResolvable[] globalParams)
            where TService : class
        {
            if (customizationFunc is null)
                throw new ArgumentNullException(nameof(customizationFunc));

            var builder = new GenericDecoratorBasedServiceResolutionInfoBuilder<TService>(globalParams);
            customizationFunc(builder);
            var resolutionInfo = builder.GetResolutionInfo();

            return (TService) resolver.GetDecoratedService(resolutionInfo);
        }

        /// <inheritdoc/>
        public object GetDecoratedService(Type serviceType,
                                          Func<ICreatesDecorator, ICustomizesDecorator> customizationFunc,
                                          params ITypedResolvable[] globalParams)
        {
            if (customizationFunc is null)
                throw new ArgumentNullException(nameof(customizationFunc));

            var builder = new DecoratorBasedServiceResolutionInfoBuilder(serviceType, globalParams);
            customizationFunc(builder);
            var resolutionInfo = builder.GetResolutionInfo();

            return resolver.GetDecoratedService(resolutionInfo);
        }

        /// <summary>
        /// Initialises a new instance of <see cref="DecoratedServiceFactory"/>.
        /// </summary>
        /// <param name="resolver">An object which resolves services from a <see cref="DecoratorBasedServiceResolutionInfo"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="resolver"/> is <see langword="null" />.</exception>
        public DecoratedServiceFactory(IGetsDecoratedServiceFromResolutionInfo resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    }
}