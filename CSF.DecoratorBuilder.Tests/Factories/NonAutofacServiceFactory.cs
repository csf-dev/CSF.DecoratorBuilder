using System;
using CSF.DecoratorBuilder.SampleService;

namespace CSF.DecoratorBuilder.Factories
{
    /// <summary>
    /// Example of a factory service which does not make use of any Autofac-specific functionality
    /// to create a service using the decorator pattern.
    /// </summary>
    public class NonAutofacServiceFactory
    {
        private readonly IGetsDecoratedService builder;

        public IServiceInterface GetService(int paramValue)
        {
            return builder.GetDecoratedService<IServiceInterface>(d =>
                d.UsingInitialImpl<ServiceImpl2>()
                .ThenWrapWith<ServiceDecorator2>(TypedParam.From(paramValue))
                .ThenWrapWith<ServiceDecorator1>()
            );
        }

        public NonAutofacServiceFactory(IGetsDecoratedService builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }

}