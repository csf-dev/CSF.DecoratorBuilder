using System;
namespace CSF.DecoratorBuilder.Tests.SampleService
{
    public class ServiceDecorator1 : IServiceInterface
    {
        readonly IServiceInterface wrapped;

        public ServiceDecorator1(IServiceInterface wrapped)
        {
            this.wrapped = wrapped;
        }

        public string ServiceMethod() => $"{GetType().Name}\n{wrapped.ServiceMethod()}";
    }
}
