﻿using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    public interface ICreatesAutofacDecorator<in TService> where TService : class
    {
        ICustomizesAutofacDecorator<TService> UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams)
            where TInitialImpl : class, TService;

        ICustomizesAutofacDecorator<TService> UsingInitialImplType(Type initialImplType,
            params Parameter[] autofacParams);
    }

    public interface ICreatesAutofacDecorator
    {
        ICustomizesAutofacDecorator UsingInitialImpl<TInitialImpl>(params Parameter[] autofacParams)
            where TInitialImpl : class;

        ICustomizesAutofacDecorator UsingInitialImplType(Type initialImplType, params Parameter[] autofacParams);
    }
}