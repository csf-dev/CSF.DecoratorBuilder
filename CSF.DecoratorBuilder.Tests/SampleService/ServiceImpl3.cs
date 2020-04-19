using System;
namespace CSF.DecoratorBuilder.Tests.SampleService
{
    public class ServiceImpl3 : IServiceInterface
    {
        public string ServiceMethod() => GetType().Name;
    }
}
