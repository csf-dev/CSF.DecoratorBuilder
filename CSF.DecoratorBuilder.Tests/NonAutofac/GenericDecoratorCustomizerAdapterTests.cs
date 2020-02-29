using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using AutoFixture.NUnit3;
using CSF.DecoratorBuilder.NonAutofac;
using CSF.DecoratorBuilder.Tests.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder.Tests.NonAutofac
{
    [TestFixture,Parallelizable]
    public class GenericDecoratorCustomizerAdapterTests
    {
        [Test, AutoMoqData]
        public void ThenWrapWith_executes_appropriate_functionality_from_wrapped_service([Frozen] ICustomizesAutofacDecorator<IServiceInterface> wrapped,
                                                                                             GenericDecoratorCustomizerAdapter<IServiceInterface> sut,
                                                                                             ICustomizesAutofacDecorator<IServiceInterface> customiser)
        {
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWith<ServiceImpl1>(It.IsAny<Parameter[]>()))
                .Returns(customiser);
            sut.ThenWrapWith<ServiceImpl1>();
            Mock.Get(wrapped).Verify(x => x.ThenWrapWith<ServiceImpl1>(), Times.Once);
        }

        [Test, AutoMoqData]
        public void ThenWrapWithType_executes_appropriate_functionality_from_wrapped_service([Frozen] ICustomizesAutofacDecorator<IServiceInterface> wrapped,
                                                                                                 GenericDecoratorCustomizerAdapter<IServiceInterface> sut,
                                                                                                 Type aType,
                                                                                                 ICustomizesAutofacDecorator<IServiceInterface> customiser)
        {
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWithType(It.IsAny<Type>(), It.IsAny<Parameter[]>()))
                .Returns(customiser);
            sut.ThenWrapWithType(aType);
            Mock.Get(wrapped).Verify(x => x.ThenWrapWithType(aType), Times.Once);
        }

        [Test, AutoMoqData]
        public void ThenWrapWith_maps_and_passes_parameters([Frozen] ICustomizesAutofacDecorator<IServiceInterface> wrapped,
                                                                GenericDecoratorCustomizerAdapter<IServiceInterface> sut,
                                                                ICustomizesAutofacDecorator<IServiceInterface> customiser)
        {
            Parameter[] parameters = null;
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWith<ServiceImpl1>(It.IsAny<Parameter[]>()))
                .Callback((Parameter[] @params) => parameters = @params)
                .Returns(customiser);
            sut.ThenWrapWith<ServiceImpl1>(TypedParam.From("Foo bar"), TypedParam.From(5));

            IEqualityComparer<Parameter> comparer = new TypedParameterComparer();
            var expected = new[]
            {
                TypedParameter.From("Foo bar"),
                TypedParameter.From(5),
            };
            Assert.That(parameters, Is.EqualTo(expected).Using(comparer));
        }

        [Test, AutoMoqData]
        public void ThenWrapWithType_maps_and_passes_parameters([Frozen] ICustomizesAutofacDecorator<IServiceInterface> wrapped,
                                                                    GenericDecoratorCustomizerAdapter<IServiceInterface> sut,
                                                                    Type aType,
                                                                    ICustomizesAutofacDecorator<IServiceInterface> customiser)
        {
            Parameter[] parameters = null;
            Mock.Get(wrapped)
                .Setup(x => x.ThenWrapWithType(It.IsAny<Type>(), It.IsAny<Parameter[]>()))
                .Callback((Type t, Parameter[] @params) => parameters = @params)
                .Returns(customiser);
            sut.ThenWrapWithType(typeof(ServiceImpl1), TypedParam.From("Foo bar"), TypedParam.From(5));

            IEqualityComparer<Parameter> comparer = new TypedParameterComparer();
            var expected = new[]
            {
                TypedParameter.From("Foo bar"),
                TypedParameter.From(5),
            };
            Assert.That(parameters, Is.EqualTo(expected).Using(comparer));
        }
    }
}
