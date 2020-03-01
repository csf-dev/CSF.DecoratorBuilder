//
// AutofacDecoratedServiceFactory.cs
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
using CSF.DecoratorBuilder.Autofac;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A factory service which can build and resolve other services, using the decorator pattern.
    /// This implementation directly references Autofac 'concepts' and requires a reference to the Autofac assemblies.
    /// </summary>
    public class AutofacDecoratedServiceFactory : IGetsAutofacDecoratedService
    {
        readonly IResolver resolver;

        /// <summary>
        /// Gets a service using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        public TService GetDecoratedService<TService>(Func<ICreatesAutofacDecorator<TService>, ICustomizesAutofacDecorator<TService>> customizationFunc) where TService : class
        {
            var builder = new AutofacGenericDecoratorBuilder<TService>(resolver);
            var customizer = (IGetsService<TService>) customizationFunc(builder);
            return customizer.GetService();
        }

        /// <summary>
        /// Gets a service using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        public object GetDecoratedService(Type serviceType, Func<ICreatesAutofacDecorator, ICustomizesAutofacDecorator> customizationFunc)
        {
            var builder = new AutofacDecoratorBuilder(resolver, serviceType);
            var customizer = (IGetsService) customizationFunc(builder);
            return customizer.GetService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacDecoratedServiceFactory"/> class.
        /// </summary>
        /// <param name="resolver">A resolver service by which further components may be retrieved where required.</param>
        public AutofacDecoratedServiceFactory(IResolver resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    }
}
