//
// GenericDecoratorCustomizerAdapter.cs
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
    /// Implementation of a decorator-customization service which makes NO direct use of Autofac in its public API,
    /// except for its constructor.  Consume this class via <see cref="IGetsDecoratedService"/>.
    /// </summary>
    public class GenericDecoratorCustomizerAdapter<TService> : ICustomizesDecorator<TService>, IGetsService<TService> where TService : class
    {
        readonly ICustomizesAutofacDecorator<TService> builder;

        TService IGetsService<TService>.GetService() => ((IGetsService<TService>) builder).GetService();

        /// <summary>
        /// Selects a decorator type using a generic type parameter.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="parameters">An optional collection of <see cref="TypedParam"/>.</param>
        /// <typeparam name="TDecorator">The type of the concrete implementation to use as a decorator.</typeparam>
        public ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(params TypedParam[] parameters) where TDecorator : class, TService
        {
            var customizer = builder.ThenWrapWith<TDecorator>(parameters?.Select(x => new TypedParameter(x.Type, x.Value)).ToArray() ?? new TypedParameter[0]);
            return new GenericDecoratorCustomizerAdapter<TService>(customizer);
        }

        /// <summary>
        /// Selects a decorator type.  The implementation directly
        /// before this point in the decorator 'stack' (be it the initial implementation or a
        /// decorator itself) will be passed to the selected implementation.  Thus this implementation
        /// will 'wrap' the one before it.
        /// </summary>
        /// <returns>A customisation helper by which further implementations may be added to the decorator 'stack'.</returns>
        /// <param name="decoratorType">The type of the concrete implementation to use as a decorator.</param>
        /// <param name="parameters">An optional collection of <see cref="TypedParam"/>.</param>
        public ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType, params TypedParam[] parameters)
        {
            var customizer = builder.ThenWrapWithType(decoratorType, parameters?.Select(x => new TypedParameter(x.Type, x.Value)).ToArray() ?? new TypedParameter[0]);
            return new GenericDecoratorCustomizerAdapter<TService>(customizer);
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="GenericDecoratorCustomizerAdapter{TService}"/> class.
        /// </summary>
        /// <param name="customizer">A wrapped autofac-specific decorator-customizer.</param>
        public GenericDecoratorCustomizerAdapter(ICustomizesAutofacDecorator<TService> customizer)
        {
            builder = customizer ?? throw new ArgumentNullException(nameof(customizer));
        }
    }
}
