//
// AutofacDecoratorBuilderTests.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2019 Craig Fowler
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using CSF.DecoratorBuilder.Autofac;
using CSF.DecoratorBuilder.Tests.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder.Tests.Autofac
{
    [TestFixture,Parallelizable]
    public class AutofacGenericDecoratorBuilderTests
    {
        #region UsingInitialImpl

        [Test, AutoMoqData]
        public void UsingInitialImpl_resolves_impl_from_resolver(IResolver resolver, ServiceImpl1 impl)
        {
            var sut = new AutofacGenericDecoratorBuilder<IServiceInterface>(resolver);
            Mock.Get(resolver)
                .Setup(x => x.Resolve<ServiceImpl1>(It.IsAny<IEnumerable<Parameter>>()))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.UsingInitialImpl<ServiceImpl1>();

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void UsingInitialImpl_passes_params_to_resolver(IResolver resolver,
                                                               ServiceImpl1 impl,
                                                               Parameter[] parameters)
        {
            var sut = new AutofacGenericDecoratorBuilder<IServiceInterface>(resolver);
            Mock.Get(resolver)
                .Setup(x => x.Resolve<ServiceImpl1>(parameters))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.UsingInitialImpl<ServiceImpl1>(parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        #endregion

        #region UsingInitialImplType

        [Test, AutoMoqData]
        public void UsingInitialImplType_throws_exception_if_impl_type_does_not_derive_from_service(IResolver resolver)
        {
            var sut = new AutofacGenericDecoratorBuilder<IServiceInterface>(resolver);

            Assert.That(() => sut.UsingInitialImplType(typeof(DifferentImpl)), Throws.ArgumentException);
        }

        [Test, AutoMoqData]
        public void UsingInitialImplType_resolves_impl_from_resolver(IResolver resolver, ServiceImpl1 impl)
        {
            var sut = new AutofacGenericDecoratorBuilder<IServiceInterface>(resolver);
            Mock.Get(resolver)
                .Setup(x => x.Resolve(typeof(ServiceImpl1), It.IsAny<IEnumerable<Parameter>>()))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.UsingInitialImplType(typeof(ServiceImpl1));

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void UsingInitialImplType_passes_params_to_resolver(IResolver resolver,
                                                                   ServiceImpl1 impl,
                                                                   Parameter[] parameters)
        {
            var sut = new AutofacGenericDecoratorBuilder<IServiceInterface>(resolver);
            Mock.Get(resolver)
                .Setup(x => x.Resolve(typeof(ServiceImpl1), parameters))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.UsingInitialImplType(typeof(ServiceImpl1), parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        #endregion
    }
}
