using System;
using Autofac;
using CSF.DecoratorBuilder.SampleService;

namespace CSF.DecoratorBuilder.Factories
{
    /// <summary>
    /// Example of a factory service which makes use of Autofac functionality to create a service using the decorator pattern.
    /// </summary>
    public class AutofacServiceFactory
    {
        private readonly IGetsAutofacDecoratedService builder;

        public IServiceInterface GetService(int paramValue)
        {
            return builder.GetDecoratedService<IServiceInterface>(d =>
                d.UsingInitialImpl<ServiceImpl2>()
                 .ThenWrapWith<ServiceDecorator2>(new NamedParameter("aParam", paramValue))
                 .ThenWrapWith<ServiceDecorator1>()
            );
        }

        public AutofacServiceFactory(IGetsAutofacDecoratedService builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}