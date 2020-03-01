//
// IResolver.cs
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
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    /// <summary>
    /// An abstraction for a service which can resolve other services.  This is not intended for use in consuming code.
    /// In practice this is an abstraction of Autofac <c>IComponentContext</c>, exposing a limited subset of its
    /// functionality.
    /// </summary>
    public interface IResolver
    {
        /// <summary>
        /// Resolve a service, with a collection of parameters.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="parameters">Parameters.</param>
        /// <typeparam name="TService">The service type.</typeparam>
        TService Resolve<TService>(IEnumerable<Parameter> parameters);

        /// <summary>
        /// Resolve a service, with a collection of parameters.
        /// </summary>
        /// <returns>The resolved service.</returns>
        /// <param name="serviceType">The service type.</param>
        /// <param name="parameters">Parameters.</param>
        object Resolve(Type serviceType, IEnumerable<Parameter> parameters);
    }
}
