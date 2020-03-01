using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    /// <summary>
    /// Generic/type-safe implementation of a decorator-builder service, which makes direct
    /// use of Autofac in its public API.  In essence this class is an ADAPTER for the
    /// <see cref="AutofacDecoratorBuilder"/> service.
    /// Consume this class via <see cref="IGetsAutofacDecoratedService"/>.
    /// </summary>
    /// <typeparam name="TService">The service type to be created by the current instance.</typeparam>
    public class AutofacGenericDecoratorBuilder<TService> : ICreatesAutofacDecorator<TService> where TService : class
    {
        readonly AutofacDecoratorBuilder builder;

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TInitialImpl">The type of the initial concrete implementation.</typeparam>
        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : class,TService
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        /// <summary>
        /// Selects the initial implementation type.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="initialImplType">The type of the initial concrete implementation.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            var customizer = builder.UsingInitialImplType(initialImplType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AutofacGenericDecoratorBuilder{TService}"/> class.
        /// </summary>
        /// <param name="resolver">A resolver service by which further components may be retrieved where required.</param>
        public AutofacGenericDecoratorBuilder(IResolver resolver)
        {
            builder = new AutofacDecoratorBuilder(resolver, typeof(TService));
        }
    }
}