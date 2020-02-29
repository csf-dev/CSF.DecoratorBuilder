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
    public class DecoratorBuilderAdapterTests
    {
        [Test, AutoMoqData]
        public void UsingInitialImpl_executes_appropriate_functionality_from_wrapped_service([Frozen] ICreatesAutofacDecorator wrapped,
                                                                                             DecoratorBuilderAdapter sut,
                                                                                             ICustomizesAutofacDecorator customiser)
        {
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImpl<ServiceImpl1>(It.IsAny<Parameter[]>()))
                .Returns(customiser);
            sut.UsingInitialImpl<ServiceImpl1>();
            Mock.Get(wrapped).Verify(x => x.UsingInitialImpl<ServiceImpl1>(), Times.Once);
        }

        [Test, AutoMoqData]
        public void UsingInitialImplType_executes_appropriate_functionality_from_wrapped_service([Frozen] ICreatesAutofacDecorator wrapped,
                                                                                                 DecoratorBuilderAdapter sut,
                                                                                                 Type aType,
                                                                                                 ICustomizesAutofacDecorator customiser)
        {
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImplType(It.IsAny<Type>(), It.IsAny<Parameter[]>()))
                .Returns(customiser);
            sut.UsingInitialImplType(aType);
            Mock.Get(wrapped).Verify(x => x.UsingInitialImplType(aType), Times.Once);
        }

        [Test, AutoMoqData]
        public void UsingInitialImpl_maps_and_passes_parameters([Frozen] ICreatesAutofacDecorator wrapped,
                                                                DecoratorBuilderAdapter sut,
                                                                ICustomizesAutofacDecorator customiser)
        {
            Parameter[] parameters = null;
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImpl<ServiceImpl1>(It.IsAny<Parameter[]>()))
                .Callback((Parameter[] @params) => parameters = @params)
                .Returns(customiser);
            sut.UsingInitialImpl<ServiceImpl1>(TypedParam.From("Foo bar"), TypedParam.From(5));

            IEqualityComparer<Parameter> comparer = new TypedParameterComparer();
            var expected = new[]
            {
                TypedParameter.From("Foo bar"),
                TypedParameter.From(5),
            };
            Assert.That(parameters, Is.EqualTo(expected).Using(comparer));
        }

        [Test, AutoMoqData]
        public void UsingInitialImplType_maps_and_passes_parameters([Frozen] ICreatesAutofacDecorator wrapped,
                                                                    DecoratorBuilderAdapter sut,
                                                                    Type aType,
                                                                    ICustomizesAutofacDecorator customiser)
        {
            Parameter[] parameters = null;
            Mock.Get(wrapped)
                .Setup(x => x.UsingInitialImplType(It.IsAny<Type>(), It.IsAny<Parameter[]>()))
                .Callback((Type t, Parameter[] @params) => parameters = @params)
                .Returns(customiser);
            sut.UsingInitialImplType(typeof(ServiceImpl1), TypedParam.From("Foo bar"), TypedParam.From(5));

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
