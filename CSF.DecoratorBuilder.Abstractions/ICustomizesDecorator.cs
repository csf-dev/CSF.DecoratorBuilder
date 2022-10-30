using System;
using System.Collections.Generic;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A builder/helper object by which a developer selects the decorator types to be used
    /// when creating the service.
    /// </summary>
    /// <typeparam name="TService">The service type, typically an interface.</typeparam>
    public interface ICustomizesDecorator<TService> where TService : class
    {
        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters)
            where TDecorator : class, TService;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType, params ITypedResolvable[] parameters);

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(Func<TService,IServiceProvider,IEnumerable<ITypedResolvable>,TDecorator> factoryFunction,
                                                                params ITypedResolvable[] parameters)
            where TDecorator : class, TService;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType,
                                                        Func<TService,IServiceProvider,IEnumerable<ITypedResolvable>,TService> factoryFunction,
                                                        params ITypedResolvable[] parameters);
    }

    /// <summary>
    /// A builder/helper object by which a developer selects the decorator types to be used
    /// when creating the service.
    /// </summary>
    public interface ICustomizesDecorator
    {
        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesDecorator ThenWrapWith<TDecorator>(params ITypedResolvable[] parameters)
            where TDecorator : class;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        ICustomizesDecorator ThenWrapWithType(Type decoratorType,
                                              params ITypedResolvable[] parameters);

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesDecorator ThenWrapWith<TDecorator>(Func<object,IServiceProvider,IEnumerable<ITypedResolvable>,TDecorator> factoryFunction,
                                                      params ITypedResolvable[] parameters)
            where TDecorator : class;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="ITypedResolvable"/>.</param>
        ICustomizesDecorator ThenWrapWithType(Type decoratorType,
                                              Func<object,IServiceProvider,IEnumerable<ITypedResolvable>,object> factoryFunction,
                                              params ITypedResolvable[] parameters);
    }
}