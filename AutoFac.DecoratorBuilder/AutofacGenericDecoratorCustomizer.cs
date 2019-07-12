using System;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class AutofacGenericDecoratorCustomizer<TService> : ICustomizesAutofacDecorator<TService> where TService : class
    {
        readonly IComponentContext context;

        public TService Implementation { get; }

        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : TService
        {
            var decoratorImpl = context.Resolve<TDecorator>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(context, decoratorImpl);
        }

        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType<TService>(decoratorType))
                throw new ArgumentException($"The decorator type {decoratorType.FullName} must derive from the service type {typeof(TService).FullName}.", nameof(decoratorType));

            var decoratorImpl = (TService) context.Resolve(decoratorType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(context, decoratorImpl);
        }

        public AutofacGenericDecoratorCustomizer(IComponentContext context, TService implementation)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}