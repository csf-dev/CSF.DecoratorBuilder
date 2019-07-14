using System;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class AutofacGenericDecoratorCustomizer<TService> : ICustomizesAutofacDecorator<TService> where TService : class
    {
        readonly IResolver resolver;

        public TService Implementation { get; }

        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : TService
        {
            var decoratorImpl = resolver.Resolve<TDecorator>(autofacParams.AddTypedParam(Implementation));
            return new AutofacGenericDecoratorCustomizer<TService>(resolver, decoratorImpl);
        }

        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType<TService>(decoratorType))
                throw new ArgumentException($"The decorator type {decoratorType.FullName} must derive from the service type {typeof(TService).FullName}.", nameof(decoratorType));

            var decoratorImpl = (TService) resolver.Resolve(decoratorType, autofacParams.AddTypedParam(Implementation));
            return new AutofacGenericDecoratorCustomizer<TService>(resolver, decoratorImpl);
        }

        public AutofacGenericDecoratorCustomizer(IResolver resolver, TService implementation)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}