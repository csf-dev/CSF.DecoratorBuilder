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
using System.Linq;
using Autofac;
using Autofac.Core;
using CSF.DecoratorBuilder.Autofac;
using CSF.DecoratorBuilder.Tests.SampleService;
using Moq;
using NUnit.Framework;

namespace CSF.DecoratorBuilder.Tests.Autofac
{
    [TestFixture,Parallelizable]
    public class AutofacGenericDecoratorCustomizerTests
    {
        #region ThenWrapWith

        [Test, AutoMoqData]
        public void ThenWrapWith_resolves_impl_from_resolver(IResolver resolver,
                                                                 ServiceImpl2 impl,
                                                                 ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve<ServiceImpl2>(It.IsAny<IEnumerable<Parameter>>()))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWith<ServiceImpl2>();

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void ThenWrapWith_passes_params_to_resolver(IResolver resolver,
                                                              ServiceImpl2 impl,
                                                              Parameter[] parameters,
                                                              ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve<ServiceImpl2>(It.Is<IEnumerable<Parameter>>(p => parameters.All(y => p.Contains(y)))))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWith<ServiceImpl2>(parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void ThenWrapWith_passes_impl_to_resolver_as_parameter(IResolver resolver,
                                                                      ServiceImpl2 impl,
                                                                      Parameter[] parameters,
                                                                      ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve<ServiceImpl2>(It.Is<IEnumerable<Parameter>>(p => p.OfType<TypedParameter>().Any(a => a.Type == typeof(IServiceInterface) && a.Value == initialImpl))))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWith<ServiceImpl2>(parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        #endregion

        #region ThenWrapWithType

        [Test, AutoMoqData]
        public void ThenWrapWithType_throws_exception_if_impl_type_does_not_derive_from_service(IResolver resolver,
                                                                                                    ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);

            Assert.That(() => sut.ThenWrapWithType(typeof(DifferentImpl)), Throws.ArgumentException);
        }

        [Test, AutoMoqData]
        public void ThenWrapWithType_resolves_impl_from_resolver(IResolver resolver,
                                                                    ServiceImpl2 impl,
                                                                    ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve(typeof(ServiceImpl2), It.IsAny<IEnumerable<Parameter>>()))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWithType(typeof(ServiceImpl2));

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void ThenWrapWithType_passes_params_to_resolver(IResolver resolver,
                                                                  ServiceImpl2 impl,
                                                                  Parameter[] parameters,
                                                                  ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve(typeof(ServiceImpl2), It.Is<IEnumerable<Parameter>>(p => parameters.All(y => p.Contains(y)))))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWithType(typeof(ServiceImpl2), parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        [Test, AutoMoqData]
        public void ThenWrapWithType_passes_impl_to_resolver_as_parameter(IResolver resolver,
                                                                          ServiceImpl2 impl,
                                                                          Parameter[] parameters,
                                                                          ServiceImpl1 initialImpl)
        {
            var wrapped = new AutofacDecoratorCustomizer(resolver, typeof(IServiceInterface), initialImpl);
            var sut = new AutofacGenericDecoratorCustomizer<IServiceInterface>(wrapped, initialImpl);
            Mock.Get(resolver)
                .Setup(x => x.Resolve(typeof(ServiceImpl2), It.Is<IEnumerable<Parameter>>(p => p.OfType<TypedParameter>().Any(a => a.Type == typeof(IServiceInterface) && a.Value == initialImpl))))
                .Returns(impl);

            var result = (AutofacGenericDecoratorCustomizer<IServiceInterface>) sut.ThenWrapWithType(typeof(ServiceImpl2), parameters);

            Assert.That(result?.Implementation, Is.SameAs(impl));
        }

        #endregion
    }
}
