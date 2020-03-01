//
// DecoratorBuilder.cs
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

namespace CSF.DecoratorBuilder.NonAutofac
{
    /// <summary>
    /// Implementation of a decorator-builder service which makes NO direct use of Autofac in its public API,
    /// except for its constructor.  Consume this class via <see cref="IGetsDecoratedService"/>.
    /// </summary>
    public class DecoratorBuilderAdapter : ICreatesDecorator
    {
        readonly ICreatesAutofacDecorator builder;

        /// <summary>
        /// Selects the initial implementation type using a generic type parameter.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="TypedParam"/>.</param>
        /// <typeparam name="TInitialImpl">The type of the initial concrete implementation.</typeparam>
        public ICustomizesDecorator UsingInitialImpl<TInitialImpl>(params TypedParam[] parameters) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(parameters?.Select(x => new TypedParameter(x.Type, x.Value)).ToArray() ?? new TypedParameter[0]);
            return new DecoratorCustomizerAdapter(customizer);
        }

        /// <summary>
        /// Selects the initial implementation type.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="initialImplType">The type of the initial concrete implementation.</param>
        /// <param name="parameters">An optional collection of <see cref="TypedParam"/>.</param>
        public ICustomizesDecorator UsingInitialImplType(Type initialImplType, params TypedParam[] parameters)
        {
            var customizer = builder.UsingInitialImplType(initialImplType, parameters?.Select(x => new TypedParameter(x.Type, x.Value)).ToArray() ?? new TypedParameter[0]);
            return new DecoratorCustomizerAdapter(customizer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorBuilderAdapter"/> class.
        /// </summary>
        /// <param name="builder">A wrapped autofac-specific decorator-builder.</param>
        public DecoratorBuilderAdapter(ICreatesAutofacDecorator builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
