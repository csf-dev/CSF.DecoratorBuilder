using System;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class AutofacGenericDecoratorBuilder<TService> : ICreatesAutofacDecorator<TService> where TService : class
    {
        readonly IResolver resolver;

        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : TService
        {
            var initialImpl = resolver.Resolve<TInitialImpl>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(resolver, initialImpl);
        }

        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType<TService>(initialImplType))
                throw new ArgumentException($"The implementation type {initialImplType.FullName} must derive from the service type {typeof(TService).FullName}.", nameof(initialImplType));

            var initialImpl = (TService) resolver.Resolve(initialImplType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(resolver, initialImpl);
        }

        public AutofacGenericDecoratorBuilder(IResolver resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    }
}