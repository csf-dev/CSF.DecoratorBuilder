using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A builder/helper object by which a developer selects the initial concrete implementation type for
    /// the innermost object in the decorator 'stack'.  This is the one class in the stack which is itself
    /// not a decorator type.
    /// </summary>
    /// <typeparam name="TService">The service type, typically an interface.</typeparam>
    public interface ICreatesAutofacDecorator<TService> where TService : class
    {
        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TInitialImpl">The type of the initial concrete implementation.</typeparam>
        ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams)
            where TInitialImpl : class, TService;

        /// <summary>
        /// Selects the initial implementation type.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="initialImplType">The type of the initial concrete implementation.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType,
            params Parameter[] autofacParams);

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the initial implementation object.</param>
        ICustomizesAutofacDecorator<TService> UsingInitialImpl(Func<IComponentContext, TService> resolverFunc);
    }

    /// <summary>
    /// A builder/helper object by which a developer selects the initial concrete implementation type for
    /// the innermost object in the decorator 'stack'.  This is the one class in the stack which is itself
    /// not a decorator type.
    /// </summary>
    public interface ICreatesAutofacDecorator
    {
        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TInitialImpl">The type of the initial concrete implementation.</typeparam>
        ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams)
            where TInitialImpl : class;

        /// <summary>
        /// Selects the initial implementation type.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="initialImplType">The type of the initial concrete implementation.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams);

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the initial implementation object.</param>
        ICustomizesAutofacDecorator UsingInitialImpl(Func<IComponentContext, object> resolverFunc);
    }
}