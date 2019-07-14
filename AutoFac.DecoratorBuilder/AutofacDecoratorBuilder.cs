using System;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class AutofacDecoratorBuilder : ICreatesAutofacDecorator
    {
        readonly IResolver resolver;
        readonly Type serviceType;

        public ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : class
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(typeof(TInitialImpl), serviceType))
                throw new ArgumentException($"The implementation type {typeof(TInitialImpl).FullName} must derive from the service type {serviceType.FullName}.");

            var initialImpl = resolver.Resolve<TInitialImpl>(autofacParams);
            return new AutofacDecoratorCustomizer(resolver, serviceType, initialImpl);
        }

        public ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(initialImplType, serviceType))
                throw new ArgumentException($"The implementation type {initialImplType.FullName} must derive from the service type {serviceType.FullName}.");

            var initialImpl = resolver.Resolve(initialImplType, autofacParams);
            return new AutofacDecoratorCustomizer(resolver, serviceType, initialImpl);
        }

        public AutofacDecoratorBuilder(IResolver resolver, Type serviceType)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
        }
    }
}