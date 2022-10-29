using System.Linq;
using CSF.DecoratorBuilder.SampleService;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture,Parallelizable]
    public class DecoratorBasedServiceResolutionInfoTests
    {
        [Test,AutoMoqData]
        public void EnqueueServiceResolutionInfoShouldThrowIfTheImplTypeIsNull()
        {
            var sut = new DecoratorBasedServiceResolutionInfo(typeof(IServiceInterface));
            Assert.That(() => sut.EnqueueServiceResolutionInfo(null, Enumerable.Empty<ITypedResolvable>()), Throws.ArgumentNullException);
        }

        [Test,AutoMoqData]
        public void EnqueueServiceResolutionInfoShouldThrowIfTheImplTypeDoesNotDeriveFromTheServiceType()
        {
            var sut = new DecoratorBasedServiceResolutionInfo(typeof(IServiceInterface));
            Assert.That(() => sut.EnqueueServiceResolutionInfo(typeof(object), Enumerable.Empty<ITypedResolvable>()), Throws.ArgumentException);
        }

        [Test,AutoMoqData]
        public void EnqueueServiceResolutionInfoShouldAddAServiceResolutionInfoToTheQueue()
        {
            var sut = new DecoratorBasedServiceResolutionInfo(typeof(IServiceInterface));
            sut.EnqueueServiceResolutionInfo(typeof(ServiceImpl1), Enumerable.Empty<ITypedResolvable>());
            Assert.That(sut.ServicesToResolve, Has.Count.EqualTo(1));
        }
    }
}