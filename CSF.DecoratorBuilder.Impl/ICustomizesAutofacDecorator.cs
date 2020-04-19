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
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
            where TDecorator : class, TService;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams);

        /// <summary>
        /// Selects a decorator using a resolver function.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the decorator object.</param>
        ICustomizesAutofacDecorator<TService> ThenWrapWith(Func<TService, IComponentContext, TService> resolverFunc);
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
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
            where TDecorator : class;

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams);

        /// <summary>
        /// Selects a decorator using a resolver function.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the decorator object.</param>
        ICustomizesAutofacDecorator ThenWrapWith(Func<object, IComponentContext, object> resolverFunc);
    }
}