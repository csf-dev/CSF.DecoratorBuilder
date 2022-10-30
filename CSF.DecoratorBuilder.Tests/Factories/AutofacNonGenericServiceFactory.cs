using System;
using Autofac;
using CSF.DecoratorBuilder.SampleService;

namespace CSF.DecoratorBuilder.Factories
{
    /// <summary>
    /// Example of a factory service which makes use of Autofac functionality to create a service using the
    /// decorator pattern, without using generic types.
    /// </summary>
    public class AutofacNonGenericServiceFactory
    {
        private readonly IGetsAutofacDecoratedService builder;

        public object GetService(int paramValue)
        {
            return builder.GetDecoratedService(typeof(IServiceInterface), d =>
                d.UsingInitialImplType(typeof(ServiceImpl2))
                 .ThenWrapWithType(typeof(ServiceDecorator2), new NamedParameter("aParam", paramValue))
                 .ThenWrapWithType(typeof(ServiceDecorator1))
            );
        }

        public AutofacNonGenericServiceFactory(IGetsAutofacDecoratedService builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}