using System;
using CSF.DecoratorBuilder.SampleService;

namespace CSF.DecoratorBuilder.Factories
{
    /// <summary>
    /// Example of a factory service which does not make use of Autofac functionality to create a service using the
    /// decorator pattern, without using generic types.
    /// </summary>
    public class NonAutofacNonGenericServiceFactory
    {
        private readonly IGetsDecoratedService builder;

        public object GetService(int paramValue)
        {
            return builder.GetDecoratedService(typeof(IServiceInterface), d =>
                d.UsingInitialImplType(typeof(ServiceImpl2))
                 .ThenWrapWithType(typeof(ServiceDecorator2), TypedParam.From(paramValue))
                 .ThenWrapWithType(typeof(ServiceDecorator1))
            );
        }

        public NonAutofacNonGenericServiceFactory(IGetsDecoratedService builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}