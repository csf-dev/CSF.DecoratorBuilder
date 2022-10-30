using System;
using System.Collections.Generic;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoFixture.NUnit3;
using CSF.DecoratorBuilder.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder.Tests
{
    [TestFixture,Parallelizable]
    public class AutofacDecoratorBasedServiceResolutionInfoBuilderAdapterTests
    {
        [Test,AutoMoqData]
        public void UsingInitialImplShouldCallWrappedService([Frozen] IDecoratorBuilder wrapped,
                                                             AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                             Parameter param1,
                                                             Parameter param2)
        {
            sut.UsingInitialImpl<IServiceInterface>(param1, param2);
            Mock.Get(wrapped).Verify(x => x.UsingInitialImpl<IServiceInterface>(It.Is<ITypedResolvable[]>(x => x.Length == 2)));
        }

        [Test,AutoMoqData]
        public void UsingInitialImplTypeShouldCallWrappedService([Frozen] IDecoratorBuilder wrapped,
                                                                 AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                 Parameter param1,
                                                                 Parameter param2)
        {
            sut.UsingInitialImplType(typeof(IServiceInterface), param1, param2);
            Mock.Get(wrapped).Verify(x => x.UsingInitialImplType(typeof(IServiceInterface), It.Is<ITypedResolvable[]>(x => x.Length == 2)));
        }

        [Test,AutoMoqData]
        public void ThenWrapWithShouldCallWrappedService([Frozen] IDecoratorBuilder wrapped,
                                                             AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                             Parameter param1,
                                                             Parameter param2)
        {
            sut.ThenWrapWith<IServiceInterface>(param1, param2);
            Mock.Get(wrapped).Verify(x => x.ThenWrapWith<IServiceInterface>(It.Is<ITypedResolvable[]>(x => x.Length == 2)));
        }

        [Test,AutoMoqData]
        public void ThenWrapWithTypeShouldCallWrappedService([Frozen] IDecoratorBuilder wrapped,
                                                                 AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                 Parameter param1,
                                                                 Parameter param2)
        {
            sut.ThenWrapWithType(typeof(IServiceInterface), param1, param2);
            Mock.Get(wrapped).Verify(x => x.ThenWrapWithType(typeof(IServiceInterface), It.Is<ITypedResolvable[]>(x => x.Length == 2)));
        }

        [Test,AutoMoqData]
        public void UsingInitialImplWithFunctionShouldCallWrappedServiceWithWrapperFunction([Frozen] IDecoratorBuilder wrapped,
                                                                                            AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                                            Parameter param1,
                                                                                            Parameter param2,
                                                                                            AutofacServiceProvider autofacServiceProvider)
        {
            var functionCalled = false;
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface> capturedFunc = null;
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImpl<IServiceInterface>(It.IsAny<Func<IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface>>(),
                                                                  It.IsAny<ITypedResolvable[]>()))
                .Callback((Func<IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface> func, ITypedResolvable[] @params) => capturedFunc = func);
            
            sut.UsingInitialImpl<IServiceInterface>((ctx, @params) => { functionCalled = true; return null; }, param1, param2);

            capturedFunc?.Invoke(autofacServiceProvider, Array.Empty<ITypedResolvable>());
            Assert.That(functionCalled, Is.True);
        }

        [Test,AutoMoqData]
        public void UsingInitialImplTypeWithFunctionShouldCallWrappedServiceWithWrapperFunction([Frozen] IDecoratorBuilder wrapped,
                                                                                                AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                                                Parameter param1,
                                                                                                Parameter param2,
                                                                                                AutofacServiceProvider autofacServiceProvider)
        {
            var functionCalled = false;
            Func<IServiceProvider, IEnumerable<ITypedResolvable>, object> capturedFunc = null;
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImplType(typeof(IServiceInterface),
                                                   It.IsAny<Func<IServiceProvider, IEnumerable<ITypedResolvable>, object>>(),
                                                   It.IsAny<ITypedResolvable[]>()))
                .Callback((Type type, Func<IServiceProvider, IEnumerable<ITypedResolvable>, object> func, ITypedResolvable[] @params) => capturedFunc = func);
            
            sut.UsingInitialImplType(typeof(IServiceInterface), (ctx, @params) => { functionCalled = true; return null; }, param1, param2);

            capturedFunc?.Invoke(autofacServiceProvider, Array.Empty<ITypedResolvable>());
            Assert.That(functionCalled, Is.True);
        }

        [Test,AutoMoqData]
        public void ThenWrapWithWithFunctionShouldCallWrappedServiceWithWrapperFunction([Frozen] IDecoratorBuilder wrapped,
                                                                                        AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                                        Parameter param1,
                                                                                        Parameter param2,
                                                                                        AutofacServiceProvider autofacServiceProvider)
        {
            var functionCalled = false;
            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface> capturedFunc = null;
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWith<IServiceInterface>(It.IsAny<Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface>>(),
                                                              It.IsAny<ITypedResolvable[]>()))
                .Callback((Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, IServiceInterface> func, ITypedResolvable[] @params) => capturedFunc = func);
            
            sut.ThenWrapWith<IServiceInterface>((impl, ctx, @params) => { functionCalled = true; return null; }, param1, param2);

            capturedFunc?.Invoke(null, autofacServiceProvider, Array.Empty<ITypedResolvable>());
            Assert.That(functionCalled, Is.True);
        }

        [Test,AutoMoqData]
        public void ThenWrapWithTypeWithFunctionShouldCallWrappedServiceWithWrapperFunction([Frozen] IDecoratorBuilder wrapped,
                                                                                            AutofacDecoratorBasedServiceResolutionInfoBuilderAdapter sut,
                                                                                            Parameter param1,
                                                                                            Parameter param2,
                                                                                            AutofacServiceProvider autofacServiceProvider)
        {
            var functionCalled = false;
            Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> capturedFunc = null;
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWithType(typeof(IServiceInterface),
                                               It.IsAny<Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object>>(),
                                               It.IsAny<ITypedResolvable[]>()))
                .Callback((Type type, Func<object, IServiceProvider, IEnumerable<ITypedResolvable>, object> func, ITypedResolvable[] @params) => capturedFunc = func);
            
            sut.ThenWrapWithType(typeof(IServiceInterface), (impl, ctx, @params) => { functionCalled = true; return null; }, param1, param2);

            capturedFunc?.Invoke(null, autofacServiceProvider, Array.Empty<ITypedResolvable>());
            Assert.That(functionCalled, Is.True);
        }
    }
}