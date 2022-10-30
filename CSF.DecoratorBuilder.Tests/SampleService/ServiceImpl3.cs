using System;
namespace CSF.DecoratorBuilder.SampleService
{
    public class ServiceImpl3 : IServiceInterface
    {
        public string ServiceMethod() => GetType().Name;
    }
}
