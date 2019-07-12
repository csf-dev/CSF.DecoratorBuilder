using System;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.AutoFac
{
    public class GenericAutofacDecoratorBuilder<TService> : ICreatesAutofacDecorator<TService> where TService : class
    {
        readonly IComponentContext context;

        public ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams) where TInitialImpl : TService
        {
            var initialImpl = context.Resolve<TInitialImpl>(autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(context, initialImpl);
        }

        public ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType<TService>(initialImplType))
                throw new ArgumentException($"The implementation type {initialImplType.FullName} must derive from the service type {typeof(TService).FullName}.", nameof(initialImplType));

            var initialImpl = (TService) context.Resolve(initialImplType, autofacParams);
            return new AutofacGenericDecoratorCustomizer<TService>(context, initialImpl);
        }

        public GenericAutofacDecoratorBuilder(IComponentContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}