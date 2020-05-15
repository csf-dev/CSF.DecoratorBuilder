using System;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    /// <summary>
    /// Generic/type-safe implementation of a decorator customizer service, which makes direct
    /// use of Autofac in its public API.  In essence this class is an ADAPTER for the
    /// <see cref="AutofacDecoratorCustomizer"/> service.
    /// Consume this class via <see cref="IGetsAutofacDecoratedService"/>.
    /// </summary>
    public class AutofacGenericDecoratorCustomizer<TService> : ICustomizesAutofacDecorator<TService>, IGetsService<TService> where TService : class
    {
        readonly AutofacDecoratorCustomizer builder;

        /// <summary>
        /// Gets a reference to the current implementation of the service.  This is the object
        /// which could be wrapped with further decorators.
        /// </summary>
        /// <value>The implementation instance.</value>
        public TService Implementation { get; }

        TService IGetsService<TService>.GetService() => Implementation;

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : class, TService
        {
            var customizer = builder.ThenWrapWith<TDecorator>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        /// <summary>
        /// Selects a decorator using a resolver function.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the decorator object.</param>
        public ICustomizesAutofacDecorator<TService> ThenWrapWith(Func<TService, IComponentContext, TService> resolverFunc)
        {
            var customizer = builder.ThenWrapWith((wrapped, ctx) => resolverFunc((TService)wrapped, ctx));
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService)customizer.Implementation);
        }

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            var customizer = builder.ThenWrapWithType(decoratorType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AutofacGenericDecoratorCustomizer{TService}"/> class.
        /// </summary>
        /// <param name="customizer">A wrapped decorator-customizer object.</param>
        /// <param name="implementation">The current service implementation, which could potentially be wrapped by decorators.</param>
        public AutofacGenericDecoratorCustomizer(AutofacDecoratorCustomizer customizer, TService implementation)
        {
            this.builder = customizer ?? throw new ArgumentNullException(nameof(customizer));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}