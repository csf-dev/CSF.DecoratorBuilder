using System.Linq;
using AutoFixture.NUnit3;
using CSF.DecoratorBuilder.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture,Parallelizable]
    public class DecoratedServiceFactoryTests
    {
        [Test,AutoMoqData]
        public void GetDecoratedServiceGenericShouldThrowIfCustomizationFunctionIsNull(DecoratedServiceFactory sut)
        {
            Assert.That(() => sut.GetDecoratedService<IServiceInterface>(null), Throws.ArgumentNullException);
        }

        [Test,AutoMoqData]
        public void GetDecoratedServiceGenericShouldReturnServiceFromResolverWithCustomisedResolutionInfo([Frozen] IGetsDecoratedServiceFromResolutionInfo resolver,
                                                                                                          ITypedResolvable param1,
                                                                                                          ITypedResolvable param2,
                                                                                                          DecoratedServiceFactory sut,
                                                                                                          IServiceInterface expectedResult)
        {
            Mock.Get(resolver)
                .Setup(x => x.GetDecoratedService(It.Is<DecoratorBasedServiceResolutionInfo>(i => i.ServiceType == typeof(IServiceInterface)
                                                                                               && i.ServicesToResolve.First().Type == typeof(ServiceImpl1)
                                                                                               && i.ServicesToResolve.Skip(1).First().Type == typeof(ServiceDecorator1)
                                                                                               && i.ServicesToResolve.All(x => x.Dependencies.SequenceEqual(new[] { param1, param2 })))))
                .Returns(expectedResult);
            
            Assert.That(() => sut.GetDecoratedService<IServiceInterface>(x => x.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator1>(), param1, param2),
                        Is.SameAs(expectedResult));
        }

        [Test,AutoMoqData]
        public void GetDecoratedServiceNonGenericShouldThrowIfCustomizationFunctionIsNull(DecoratedServiceFactory sut)
        {
            Assert.That(() => sut.GetDecoratedService(typeof(object), null), Throws.ArgumentNullException);
        }

        [Test,AutoMoqData]
        public void GetDecoratedServiceNonGenericShouldReturnServiceFromResolverWithCustomisedResolutionInfo([Frozen] IGetsDecoratedServiceFromResolutionInfo resolver,
                                                                                                             ITypedResolvable param1,
                                                                                                             ITypedResolvable param2,
                                                                                                             DecoratedServiceFactory sut,
                                                                                                             IServiceInterface expectedResult)
        {
            Mock.Get(resolver)
                .Setup(x => x.GetDecoratedService(It.Is<DecoratorBasedServiceResolutionInfo>(i => i.ServiceType == typeof(IServiceInterface)
                                                                                               && i.ServicesToResolve.First().Type == typeof(ServiceImpl1)
                                                                                               && i.ServicesToResolve.Skip(1).First().Type == typeof(ServiceDecorator1)
                                                                                               && i.ServicesToResolve.All(x => x.Dependencies.SequenceEqual(new[] { param1, param2 })))))
                .Returns(expectedResult);
            
            Assert.That(() => sut.GetDecoratedService(typeof(IServiceInterface), x => x.UsingInitialImpl<ServiceImpl1>().ThenWrapWith<ServiceDecorator1>(), param1, param2),
                        Is.SameAs(expectedResult));
        }
    }
}