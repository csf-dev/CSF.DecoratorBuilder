using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A builder/helper object by which a developer selects the decorator types to be used
    /// when creating the service.
    /// </summary>
    /// <typeparam name="TService">The service type, typically an interface.</typeparam>
    public interface ICustomizesAutofacDecorator<TService> where TService : class
    {
        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] parameters)
            where TDecorator : class, TService;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] parameters);

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(Func<TService,IComponentContext,IEnumerable<Parameter>,TDecorator> factoryFunction,
                                                                       params Parameter[] parameters)
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
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType,
                                                               Func<TService,IComponentContext,IEnumerable<Parameter>,TService> factoryFunction,
                                                               params Parameter[] parameters);
    }

    /// <summary>
    /// A builder/helper object by which a developer selects the decorator types to be used
    /// when creating the service.
    /// </summary>
    public interface ICustomizesAutofacDecorator
    {
        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(params Parameter[] parameters)
            where TDecorator : class;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType, params Parameter[] parameters);

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="factoryFunction">A function which creates the instance of the decorator type.</param>
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(Func<object,IComponentContext,IEnumerable<Parameter>,TDecorator> factoryFunction,
                                                             params Parameter[] parameters)
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
        /// <param name="parameters">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType,
                                                     Func<object,IComponentContext,IEnumerable<Parameter>,object> factoryFunction,
                                                     params Parameter[] parameters);
    }
}