using System.Linq;
using CSF.DecoratorBuilder.SampleService;
using NUnit.Framework;

namespace CSF.DecoratorBuilder
{
    [TestFixture,Parallelizable]
    public class GenericDecoratorBasedServiceResolutionInfoBuilderTests
    {
        [Test,AutoMoqData]
        public void UsingInitialImplShouldEnqueueServiceAndCombinationOfIndividualAndGlobalParameters(ITypedResolvable globalParam1,
                                                                                                      ITypedResolvable globalParam2,
                                                                                                      ITypedResolvable param3,
                                                                                                      ITypedResolvable param4)
        {
            var sut = new GenericDecoratorBasedServiceResolutionInfoBuilder<IServiceInterface>(globalParam1, globalParam2);
            sut.UsingInitialImpl<ServiceImpl1>(param3, param4);
            var result = sut.ResolutionInfo;
            Assert.Multiple(() =>
            {
                Assert.That(result.ServicesToResolve, Has.Count.EqualTo(1), "One service to resolve");
                var service = result.ServicesToResolve.First();
                Assert.That(service.Type, Is.EqualTo(typeof(ServiceImpl1)), "Service has correct type");
                Assert.That(service.Dependencies, Is.EquivalentTo(new [] { globalParam1, globalParam2, param3,param4 }), "Correct dependencies");
            });
        }

        [Test,AutoMoqData]
        public void UsingInitialImplTypeShouldEnqueueServiceAndCombinationOfIndividualAndGlobalParameters(ITypedResolvable globalParam1,
                                                                                                          ITypedResolvable globalParam2,
                                                                                                          ITypedResolvable param3,
                                                                                                          ITypedResolvable param4)
        {
            var sut = new GenericDecoratorBasedServiceResolutionInfoBuilder<IServiceInterface>(globalParam1, globalParam2);
            sut.UsingInitialImplType(typeof(ServiceImpl1), param3, param4);
            var result = sut.ResolutionInfo;
            Assert.Multiple(() =>
            {
                Assert.That(result.ServicesToResolve, Has.Count.EqualTo(1), "One service to resolve");
                var service = result.ServicesToResolve.First();
                Assert.That(service.Type, Is.EqualTo(typeof(ServiceImpl1)), "Service has correct type");
                Assert.That(service.Dependencies, Is.EquivalentTo(new [] { globalParam1, globalParam2, param3,param4 }), "Correct dependencies");
            });
        }

        [Test,AutoMoqData]
        public void ThenWrapWithShouldEnqueueServiceAndCombinationOfIndividualAndGlobalParameters(ITypedResolvable globalParam1,
                                                                                                  ITypedResolvable globalParam2,
                                                                                                  ITypedResolvable param3,
                                                                                                  ITypedResolvable param4)
        {
            var sut = new GenericDecoratorBasedServiceResolutionInfoBuilder<IServiceInterface>(globalParam1, globalParam2);
            sut.ThenWrapWith<ServiceImpl1>(param3, param4);
            var result = sut.ResolutionInfo;
            Assert.Multiple(() =>
            {
                Assert.That(result.ServicesToResolve, Has.Count.EqualTo(1), "One service to resolve");
                var service = result.ServicesToResolve.First();
                Assert.That(service.Type, Is.EqualTo(typeof(ServiceImpl1)), "Service has correct type");
                Assert.That(service.Dependencies, Is.EquivalentTo(new [] { globalParam1, globalParam2, param3,param4 }), "Correct dependencies");
            });
        }

        [Test,AutoMoqData]
        public void ThenWrapWithTypeShouldEnqueueServiceAndCombinationOfIndividualAndGlobalParameters(ITypedResolvable globalParam1,
                                                                                                      ITypedResolvable globalParam2,
                                                                                                      ITypedResolvable param3,
                                                                                                      ITypedResolvable param4)
        {
            var sut = new GenericDecoratorBasedServiceResolutionInfoBuilder<IServiceInterface>(globalParam1, globalParam2);
            sut.ThenWrapWithType(typeof(ServiceImpl1), param3, param4);
            var result = sut.ResolutionInfo;
            Assert.Multiple(() =>
            {
                Assert.That(result.ServicesToResolve, Has.Count.EqualTo(1), "One service to resolve");
                var service = result.ServicesToResolve.First();
                Assert.That(service.Type, Is.EqualTo(typeof(ServiceImpl1)), "Service has correct type");
                Assert.That(service.Dependencies, Is.EquivalentTo(new [] { globalParam1, globalParam2, param3,param4 }), "Correct dependencies");
            });
        }
    }
}