using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    public class AutofacGenericDecoratorBuilder<TService> : ICreatesAutofacDecorator<TService> where TService : class
    {
        readonly AutofacDecoratorBuilder builder;

        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : class,TService
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            var customizer = builder.UsingInitialImplType(initialImplType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(customizer, (TService) customizer.Implementation);
        }

        public AutofacGenericDecoratorBuilder(IResolver resolver)
        {
            builder = new AutofacDecoratorBuilder(resolver, typeof(TService));
        }
    }
}