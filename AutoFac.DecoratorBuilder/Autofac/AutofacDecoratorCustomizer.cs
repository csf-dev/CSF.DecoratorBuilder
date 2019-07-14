//
// AutofacDecoratorCustomizer.cs
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
using Autofac;
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    public class AutofacDecoratorCustomizer : ICustomizesAutofacDecorator, IGetsService
    {
        readonly IResolver resolver;
        readonly Type serviceType;

        public object Implementation { get; }

        public object GetService() => Implementation;

        public AutofacDecoratorCustomizer ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : class
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(typeof(TDecorator), serviceType))
                throw new ArgumentException($"The decorator type {typeof(TDecorator).FullName} must derive from the service type {serviceType.FullName}.");

            var decoratorImpl = resolver.Resolve<TDecorator>(autofacParams.AddTypedParam(serviceType, Implementation));
            return new AutofacDecoratorCustomizer(resolver, serviceType, decoratorImpl);
        }

        public AutofacDecoratorCustomizer ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(decoratorType, serviceType))
                throw new ArgumentException($"The decorator type {decoratorType.FullName} must derive from the service type {serviceType.FullName}.");

            var decoratorImpl = resolver.Resolve(decoratorType, autofacParams.AddTypedParam(serviceType, Implementation));
            return new AutofacDecoratorCustomizer(resolver, serviceType, decoratorImpl);
        }

        ICustomizesAutofacDecorator ICustomizesAutofacDecorator.ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
        {
            return ThenWrapWith<TDecorator>(autofacParams);
        }

        ICustomizesAutofacDecorator ICustomizesAutofacDecorator.ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            return ThenWrapWithType(decoratorType, autofacParams);
        }

        public AutofacDecoratorCustomizer(IResolver resolver, Type serviceType, object implementation)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}
