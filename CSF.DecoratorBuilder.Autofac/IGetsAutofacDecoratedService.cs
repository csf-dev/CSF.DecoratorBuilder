using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A factory service which uses the decorator pattern to build and return a service by specifying which implementation types
    /// to use and in which order.
    /// </summary>
    public interface IGetsAutofacDecoratedService
    {
        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <param name="globalParams">An optional collection of resolution parameters, to be applied to every resolution operation.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        TService GetDecoratedService<TService>(Func<ICreatesAutofacDecorator<TService>,ICustomizesAutofacDecorator<TService>> customizationFunc, params Parameter[] globalParams)
            where TService : class;

        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <param name="globalParams">An optional collection of resolution parameters, to be applied to every resolution operation.</param>
        object GetDecoratedService(Type serviceType, Func<ICreatesAutofacDecorator,ICustomizesAutofacDecorator> customizationFunc, params Parameter[] globalParams);
    }
}