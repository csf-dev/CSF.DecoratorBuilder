//
// ComponentContextResolverAdapter.cs
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

namespace CSF.DecoratorBuilder.Autofac
{
    /// <summary>
    /// An adapter type which allows an Autofac <c>IComponentContext</c> to be used
    /// as an <see cref="IResolver"/>.
    /// </summary>
    public class ComponentContextResolverAdapter : IResolver
    {
        readonly IComponentContext ctx;

        /// <summary>
        /// Resolve a service, with a collection of parameters.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="parameters">Parameters.</param>
        /// <typeparam name="TService">The service type.</typeparam>
        public TService Resolve<TService>(IEnumerable<Parameter> parameters)
        {
            return ctx.Resolve<TService>(parameters);
        }

        /// <summary>
        /// Resolve a service, with a collection of parameters.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="serviceType">The service type.</param>
        /// <param name="parameters">Parameters.</param>
        public object Resolve(Type serviceType, IEnumerable<Parameter> parameters)
        {
            return ctx.Resolve(serviceType, parameters);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ComponentContextResolverAdapter"/> class.
        /// </summary>
        /// <param name="ctx">The Autofac component context.</param>
        public ComponentContextResolverAdapter(IComponentContext ctx)
        {
            this.ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }
    }
}
