using System;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    /// <summary>
    /// Implementation of a decorator-builder service which makes direct use of Autofac in its public API.
    /// Consume this class via <see cref="IGetsAutofacDecoratedService"/>.
    /// </summary>
    public class AutofacDecoratorBuilder : ICreatesAutofacDecorator
    {
        readonly IResolver resolver;
        readonly Type serviceType;

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TInitialImpl">The type of the initial concrete implementation.</typeparam>
        public AutofacDecoratorCustomizer UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : class
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(typeof(TInitialImpl), serviceType))
                throw new ArgumentException($"The implementation type {typeof(TInitialImpl).FullName} must derive from the service type {serviceType.FullName}.");

            var initialImpl = resolver.Resolve<TInitialImpl>(autofacParams);
            return new AutofacDecoratorCustomizer(resolver, serviceType, initialImpl);
        }

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="resolverFunc">A function which is used to resolve the initial implementation object.</param>
        public AutofacDecoratorCustomizer UsingInitialImpl(Func<IComponentContext, object> resolverFunc)
        {
            if (resolverFunc == null)
                throw new ArgumentNullException(nameof(resolverFunc));

            var initialImpl = resolverFunc(resolver.GetComponentContext());
            return new AutofacDecoratorCustomizer(resolver, serviceType, initialImpl);
        }

        /// <summary>
        /// Selects the initial implementation type.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="initialImplType">The type of the initial concrete implementation.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        public AutofacDecoratorCustomizer UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(initialImplType, serviceType))
                throw new ArgumentException($"The implementation type {initialImplType.FullName} must derive from the service type {serviceType.FullName}.");

            var initialImpl = resolver.Resolve(initialImplType, autofacParams);
            return new AutofacDecoratorCustomizer(resolver, serviceType, initialImpl);
        }

        ICustomizesAutofacDecorator ICreatesAutofacDecorator.UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams)
            => UsingInitialImpl<TInitialImpl>(autofacParams);

        ICustomizesAutofacDecorator ICreatesAutofacDecorator.UsingInitialImpl(Func<IComponentContext, object> resolverFunc)
            => UsingInitialImpl(resolverFunc);

        ICustomizesAutofacDecorator ICreatesAutofacDecorator.UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
            => UsingInitialImplType(initialImplType, autofacParams);

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacDecoratorBuilder"/> class.
        /// </summary>
        /// <param name="resolver">A resolver service by which further components may be retrieved where required.</param>
        /// <param name="serviceType">The service type to be created by the current instance.</param>
        public AutofacDecoratorBuilder(IResolver resolver, Type serviceType)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
        }
    }
}