using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A factory service which uses the decorator pattern to build and return a service by specifying which implementation types
    /// to use and in which order.
    /// </summary>
    public interface IGetsDecoratedService
    {
        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc)
            where TService : class;

        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        object GetDecoratedService(Type serviceType, Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc);
    }
}