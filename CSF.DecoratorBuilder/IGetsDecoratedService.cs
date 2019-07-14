using System;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// Builds and returns a service instance, using the decorator pattern.
    /// </summary>
    public interface IGetsDecoratedService
    {
        /// <summary>
        /// Gets a service using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>,ICustomizesDecorator<TService>> customizationFunc)
            where TService : class;

        /// <summary>
        /// Gets a service using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        object GetDecoratedService(Type serviceType, Func<ICreatesDecorator,ICustomizesDecorator> customizationFunc);
    }
}