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
using Autofac;

namespace CSF.DecoratorBuilder.NonAutofac
{
    public class DecoratorBuilderAdapter : ICreatesDecorator
    {
        readonly ICreatesAutofacDecorator builder;

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl>() where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>();
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1>(TParam1 param1) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2>(TParam1 param1, TParam2 param2) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2),
                                                                    TypedParameter.From(param3));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2),
                                                                    TypedParameter.From(param3),
                                                                    TypedParameter.From(param4));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2),
                                                                    TypedParameter.From(param3),
                                                                    TypedParameter.From(param4),
                                                                    TypedParameter.From(param5));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2),
                                                                    TypedParameter.From(param3),
                                                                    TypedParameter.From(param4),
                                                                    TypedParameter.From(param5),
                                                                    TypedParameter.From(param6));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7) where TInitialImpl : class
        {
            var customizer = builder.UsingInitialImpl<TInitialImpl>(TypedParameter.From(param1),
                                                                    TypedParameter.From(param2),
                                                                    TypedParameter.From(param3),
                                                                    TypedParameter.From(param4),
                                                                    TypedParameter.From(param5),
                                                                    TypedParameter.From(param6),
                                                                    TypedParameter.From(param7));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType(Type initialImplType)
        {
            var customizer = builder.UsingInitialImplType(initialImplType);
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1>(Type initialImplType, TParam1 param1)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2>(Type initialImplType, TParam1 param1, TParam2 param2)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2),
                                                          TypedParameter.From(param3));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2),
                                                          TypedParameter.From(param3),
                                                          TypedParameter.From(param4));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2),
                                                          TypedParameter.From(param3),
                                                          TypedParameter.From(param4),
                                                          TypedParameter.From(param5));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2),
                                                          TypedParameter.From(param3),
                                                          TypedParameter.From(param4),
                                                          TypedParameter.From(param5),
                                                          TypedParameter.From(param6));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
        {
            var customizer = builder.UsingInitialImplType(initialImplType,
                                                          TypedParameter.From(param1),
                                                          TypedParameter.From(param2),
                                                          TypedParameter.From(param3),
                                                          TypedParameter.From(param4),
                                                          TypedParameter.From(param5),
                                                          TypedParameter.From(param6),
                                                          TypedParameter.From(param7));
            return new DecoratorCustomizerAdapter(customizer);
        }

        public DecoratorBuilderAdapter(ICreatesAutofacDecorator builder)
        {
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
