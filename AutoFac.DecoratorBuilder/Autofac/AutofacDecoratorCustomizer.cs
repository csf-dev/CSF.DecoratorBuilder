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
using Autofac.Core;

namespace CSF.DecoratorBuilder.Autofac
{
    /// <summary>
    /// Implementation of a decorator customizer service, which makes direct use of Autofac in its public API.
    /// Consume this class via <see cref="IGetsAutofacDecoratedService"/>.
    /// </summary>
    public class AutofacDecoratorCustomizer : ICustomizesAutofacDecorator, IGetsService
    {
        readonly IResolver resolver;
        readonly Type serviceType;

        /// <summary>
        /// Gets a reference to the current implementation of the service.  This is the object
        /// which could be wrapped with further decorators.
        /// </summary>
        /// <value>The implementation instance.</value>
        public object Implementation { get; }

        object IGetsService.GetService() => Implementation;

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        public AutofacDecoratorCustomizer ThenWrapWith<TDecorator>(params Parameter[] autofacParams) where TDecorator : class
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(typeof(TDecorator), serviceType))
                throw new ArgumentException($"The decorator type {typeof(TDecorator).FullName} must derive from the service type {serviceType.FullName}.");

            var decoratorImpl = resolver.Resolve<TDecorator>(autofacParams.AddTypedParam(serviceType, Implementation));
            return new AutofacDecoratorCustomizer(resolver, serviceType, decoratorImpl);
        }

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="autofacParams">An optional collection of <see cref="Parameter"/>.</param>
        public AutofacDecoratorCustomizer ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
        {
            if(!TypeUtilities.DoesImplTypeDeriveFromServiceType(decoratorType, serviceType))
                throw new ArgumentException($"The decorator type {decoratorType.FullName} must derive from the service type {serviceType.FullName}.");

            var decoratorImpl = resolver.Resolve(decoratorType, autofacParams.AddTypedParam(serviceType, Implementation));
            return new AutofacDecoratorCustomizer(resolver, serviceType, decoratorImpl);
        }

        ICustomizesAutofacDecorator ICustomizesAutofacDecorator.ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
        => ThenWrapWith<TDecorator>(autofacParams);

        ICustomizesAutofacDecorator ICustomizesAutofacDecorator.ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams)
            => ThenWrapWithType(decoratorType, autofacParams);

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacDecoratorCustomizer"/> class.
        /// </summary>
        /// <param name="resolver">A resolver service by which further components may be retrieved where required.</param>
        /// <param name="serviceType">The service type to be created by the current instance.</param>
        /// <param name="implementation">The current service implementation, which could potentially be wrapped by decorators.</param>
        public AutofacDecoratorCustomizer(IResolver resolver, Type serviceType, object implementation)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.serviceType = serviceType ?? throw new ArgumentNullException(nameof(serviceType));
            Implementation = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }
    }
}
