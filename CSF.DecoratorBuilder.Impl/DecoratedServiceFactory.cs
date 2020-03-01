//
// DecoratedServiceFactory.cs
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
using CSF.DecoratorBuilder.NonAutofac;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// A factory service which can build and resolve other services, using the decorator pattern.
    /// This implementation does not reference any Autofac-specific logic and DOES NOT require a reference to the Autofac assemblies.
    /// It may be consumed (via its interface) from an assembly/project which does not reference Autofac.
    /// </summary>
    public class DecoratedServiceFactory : IGetsDecoratedService
    {
        readonly IResolver resolver;

        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        public TService GetDecoratedService<TService>(Func<ICreatesDecorator<TService>, ICustomizesDecorator<TService>> customizationFunc) where TService : class
        {
            var builder = new AutofacGenericDecoratorBuilder<TService>(resolver);
            var builderAdapter = new GenericDecoratorBuilderAdapter<TService>(builder);
            var customizer = (IGetsService<TService>) customizationFunc(builderAdapter);
            return customizer.GetService();
        }

        /// <summary>
        /// Builds and returns an instance of a service, using the decorator pattern.
        /// </summary>
        /// <returns>The service instance.</returns>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        public object GetDecoratedService(Type serviceType, Func<ICreatesDecorator, ICustomizesDecorator> customizationFunc)
        {
            var builder = new AutofacDecoratorBuilder(resolver, serviceType);
            var builderAdapter = new DecoratorBuilderAdapter(builder);
            var customizer = (IGetsService) customizationFunc(builderAdapter);
            return customizer.GetService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratedServiceFactory"/> class.
        /// </summary>
        /// <param name="resolver">A resolver service by which further components may be retrieved where required.</param>
        public DecoratedServiceFactory(IResolver resolver)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    }
}
