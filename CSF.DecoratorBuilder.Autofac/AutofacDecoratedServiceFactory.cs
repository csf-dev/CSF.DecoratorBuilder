using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Implementation of <see cref="IGetsAutofacDecoratedService"/> which creates an appropriate
    /// builder, applies customisations and returns the resolved service, created using the
    /// decorator pattern.
    /// </summary>
    public class AutofacDecoratedServiceFactory : IGetsAutofacDecoratedService
    {
        readonly IGetsDecoratedServiceFromResolutionInfo resolver;

        /// <inheritdoc/>
        public TService GetDecoratedService<TService>(Func<ICreatesAutofacDecorator<TService>, ICustomizesAutofacDecorator<TService>> customizationFunc,
                                                      params Parameter[] globalParams)
            where TService : class
        {
            if (customizationFunc is null)
                throw new ArgumentNullException(nameof(customizationFunc));

            var builder = new GenericDecoratorBasedServiceResolutionInfoBuilder<TService>(globalParams.ToTypedResolvables());
            var autofacBuilder = new AutofacGenericDecoratorBasedServiceResolutionInfoBuilderAdapter<TService>(builder);
            customizationFunc(autofacBuilder);
            var resolutionInfo = autofacBuilder.GetResolutionInfo();

            return (TService) resolver.GetDecoratedService(resolutionInfo);
        }

        /// <inheritdoc/>
        public object GetDecoratedService(Type serviceType,
                                          Func<ICreatesAutofacDecorator, ICustomizesAutofacDecorator> customizationFunc,
                                          params Parameter[] globalParams)
        {
            if (customizationFunc is null)
                throw new ArgumentNullException(nameof(customizationFunc));

            var builder = new DecoratorBasedServiceResolutionInfoBuilder(serviceType, globalParams.ToTypedResolvables());
            var autofacBuilder = new AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter(builder);
            customizationFunc(autofacBuilder);
            var resolutionInfo = autofacBuilder.GetResolutionInfo();

            return resolver.GetDecoratedService(resolutionInfo);
        }

        /// <summary>
        /// Initialises a new instance of <see cref="AutofacDecoratedServiceFactory"/>.
        /// </summary>
        /// <param name="resolver">An object which resolves services from a <see cref="DecoratorBasedServiceResolutionInfo"/>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="resolver"/> is <see langword="null" />.</exception>
        public AutofacDecoratedServiceFactory(IGetsDecoratedServiceFromResolutionInfo resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    }
}