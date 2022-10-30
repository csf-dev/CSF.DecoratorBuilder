using AutoFixture.NUnit3;
using CSF.DecoratorBuilder.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture,Parallelizable]
    public class ServiceFromResolutionInfoResolverTests
    {
        [Test,AutoMoqData]
        public void GetDecoratedServiceShouldThrowIfResolutionInfoIsNull(ServiceFromResolutionInfoResolver sut)
        {
            Assert.That(() => sut.GetDecoratedService(null), Throws.ArgumentNullException);
        }

        [Test,AutoMoqData]
        public void GetDecoratedServiceShouldThrowIfResolutionInfoHasNoServices(ServiceFromResolutionInfoResolver sut)
        {
            var info = new DecoratorBasedServiceResolutionInfo(typeof(IServiceInterface));
            Assert.That(() => sut.GetDecoratedService(info), Throws.ArgumentException);
        }

        [Test,AutoMoqData]
        public void GetDecoratedServiceShouldReturnTheExpectedResultInAThreeClassDecoratorStack([Frozen] IGetsSingleObjectFromResolutionInfo resolver,
                                                                                                ServiceFromResolutionInfoResolver sut,
                                                                                                IServiceInterface result1,
                                                                                                IServiceInterface result2,
                                                                                                IServiceInterface result3)
        {
            SingleObjectResolutionInfo
                serviceInfo1 = new SingleObjectResolutionInfo(typeof(ServiceImpl1)),
                serviceInfo2 = new SingleObjectResolutionInfo(typeof(ServiceDecorator1)),
                serviceInfo3 = new SingleObjectResolutionInfo(typeof(ServiceDecorator2));
            var resolutionInfo = new DecoratorBasedServiceResolutionInfo(typeof(IServiceInterface));
            resolutionInfo.ServicesToResolve.Enqueue(serviceInfo1);
            resolutionInfo.ServicesToResolve.Enqueue(serviceInfo2);
            resolutionInfo.ServicesToResolve.Enqueue(serviceInfo3);

            Mock.Get(resolver).Setup(x => x.GetService(typeof(IServiceInterface), serviceInfo1, null)).Returns(result1);
            Mock.Get(resolver).Setup(x => x.GetService(typeof(IServiceInterface), serviceInfo2, result1)).Returns(result2);
            Mock.Get(resolver).Setup(x => x.GetService(typeof(IServiceInterface), serviceInfo3, result2)).Returns(result3);

            Assert.That(() => sut.GetDecoratedService(resolutionInfo), Is.SameAs(result3));
        }
    }
}