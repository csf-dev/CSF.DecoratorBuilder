using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    public class AutofacGenericDecoratorCustomizer<TService> : ICustomizesAutofacDecorator<TService>, IGetsService<TService> where TService : class
    {
        readonly AutofacDecoratorCustomizer builder;

        public TService Implementation { get; }

        public TService GetService() => Implementation;

        public ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : class, TService
        {
            var customizer = builder.ThenWrapWith<TDecorator>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        public ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            var customizer = builder.ThenWrapWithType(decoratorType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        public AutofacGenericDecoratorCustomizer(AutofacDecoratorCustomizer customizer, TService implementation)
        {
            this.builder = customizer ?? throw new ArgumentNullException(nameof(customizer));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}