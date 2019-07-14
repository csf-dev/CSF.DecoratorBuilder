//
// ContainerBuilderExtensions.cs
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
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Registers a service created with the decorator pattern.
        /// </summary>
        /// <returns>The registration builder.</returns>
        /// <param name="builder">A container builder.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        public static IRegistrationBuilder<TService,SimpleActivatorData,SingleRegistrationStyle>
            RegisterDecoratedService<TService>(this ContainerBuilder builder,
                                               Func<ICreatesAutofacDecorator<TService>, Parameter[], ICustomizesAutofacDecorator<TService>> customizationFunc) where TService : class
        {
            return builder.Register((ctx, parameters) => {
                var provider = ctx.Resolve<IGetsAutofacDecoratedService>();
                var afParams = parameters?.ToArray() ?? new Parameter[0];
                return provider.GetDecoratedService<TService>(creator => customizationFunc(creator, afParams));
            });
        }

        /// <summary>
        /// Registers a service created with the decorator pattern.
        /// </summary>
        /// <returns>The registration builder.</returns>
        /// <param name="builder">A container builder.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        /// <typeparam name="TService">The service type, typically an interface.</typeparam>
        public static IRegistrationBuilder<TService, SimpleActivatorData, SingleRegistrationStyle>
            RegisterDecoratedService<TService>(this ContainerBuilder builder,
                                               Func<ICreatesAutofacDecorator<TService>, ICustomizesAutofacDecorator<TService>> customizationFunc) where TService : class
        {
            return RegisterDecoratedService<TService>(builder, (creator, afParams) => customizationFunc(creator));
        }

        /// <summary>
        /// Registers a service type created with the decorator pattern.
        /// </summary>
        /// <returns>The registration builder.</returns>
        /// <param name="builder">A container builder.</param>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        public static IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle>
            RegisterDecoratedServiceType(this ContainerBuilder builder,
                                         Type serviceType,
                                         Func<ICreatesAutofacDecorator, Parameter[], ICustomizesAutofacDecorator> customizationFunc)
        {
            return builder.Register((ctx, parameters) => {
                var provider = ctx.Resolve<IGetsAutofacDecoratedService>();
                var afParams = parameters?.ToArray() ?? new Parameter[0];
                return provider.GetDecoratedService(serviceType, creator => customizationFunc(creator, afParams));
            });
        }

        /// <summary>
        /// Registers a service type created with the decorator pattern.
        /// </summary>
        /// <returns>The registration builder.</returns>
        /// <param name="builder">A container builder.</param>
        /// <param name="serviceType">The service type, typically an interface.</param>
        /// <param name="customizationFunc">A customization function, to build the 'shape' of the decorated service.</param>
        public static IRegistrationBuilder<object, SimpleActivatorData, SingleRegistrationStyle>
            RegisterDecoratedServiceType(this ContainerBuilder builder,
                                         Type serviceType,
                                         Func<ICreatesAutofacDecorator, ICustomizesAutofacDecorator> customizationFunc)
        {
            return builder.Register(ctx => {
                var provider = ctx.Resolve<IGetsAutofacDecoratedService>();
                return provider.GetDecoratedService(serviceType, customizationFunc);
            });
        }
    }
}
