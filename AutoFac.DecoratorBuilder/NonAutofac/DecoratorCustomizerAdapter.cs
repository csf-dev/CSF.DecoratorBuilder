//
// DecoratorCustomizerAdapter.cs
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

namespace CSF.DecoratorBuilder.NonAutofac
{
    public class DecoratorCustomizerAdapter : ICustomizesDecorator, IGetsService
    {
        readonly ICustomizesAutofacDecorator builder;

        public object GetService() => ((IGetsService) builder).GetService();

        public ICustomizesDecorator ThenWrapWith<TDecorator>() where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>();
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1>(TParam1 param1) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2>(TParam1 param1, TParam2 param2) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2),
                                                              TypedParameter.From(param3));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2),
                                                              TypedParameter.From(param3),
                                                              TypedParameter.From(param4));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2),
                                                              TypedParameter.From(param3),
                                                              TypedParameter.From(param4),
                                                              TypedParameter.From(param5));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2),
                                                              TypedParameter.From(param3),
                                                              TypedParameter.From(param4),
                                                              TypedParameter.From(param5),
                                                              TypedParameter.From(param6));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) where TDecorator : class
        {
            var customizer = builder.ThenWrapWith<TDecorator>(TypedParameter.From(param1),
                                                              TypedParameter.From(param2),
                                                              TypedParameter.From(param3),
                                                              TypedParameter.From(param4),
                                                              TypedParameter.From(param5),
                                                              TypedParameter.From(param6),
                                                              TypedParameter.From(param7));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType(Type decoratorType)
        {
            var customizer = builder.ThenWrapWithType(decoratorType);
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1>(Type decoratorType, TParam1 param1)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2>(Type decoratorType, TParam1 param1, TParam2 param2)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3>(Type decoratorType, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2),
                                                      TypedParameter.From(param3));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4>(Type decoratorType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2),
                                                      TypedParameter.From(param3),
                                                      TypedParameter.From(param4));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type decoratorType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2),
                                                      TypedParameter.From(param3),
                                                      TypedParameter.From(param4),
                                                      TypedParameter.From(param5));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type decoratorType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2),
                                                      TypedParameter.From(param3),
                                                      TypedParameter.From(param4),
                                                      TypedParameter.From(param5),
                                                      TypedParameter.From(param6));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type decoratorType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            var customizer = builder.ThenWrapWithType(decoratorType,
                                                      TypedParameter.From(param1),
                                                      TypedParameter.From(param2),
                                                      TypedParameter.From(param3),
                                                      TypedParameter.From(param4),
                                                      TypedParameter.From(param5),
                                                      TypedParameter.From(param6),
                                                      TypedParameter.From(param7));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public DecoratorCustomizerAdapter(ICustomizesAutofacDecorator customizer)
        {
            builder = customizer ?? throw new ArgumentNullException(nameof(customizer));
        }
    }
}
