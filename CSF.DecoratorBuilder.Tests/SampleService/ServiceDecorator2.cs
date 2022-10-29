using System;
namespace CSF.DecoratorBuilder.SampleService
{
    public class ServiceDecorator2 : IServiceInterface
    {
        readonly IServiceInterface wrapped;
        readonly int aParam;

        public ServiceDecorator2(IServiceInterface wrapped, int aParam)
        {
            this.wrapped = wrapped;
            this.aParam = aParam;
        }

        public string ServiceMethod() => $"{GetType().Name}: {aParam}\n{wrapped.ServiceMethod()}";
    }
}
