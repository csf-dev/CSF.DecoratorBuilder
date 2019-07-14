using System;
using Autofac.Core;

namespace CSF.DecoratorBuilder
{
    public interface ICustomizesAutofacDecorator<in TService> where TService : class
    {
        ICustomizesAutofacDecorator<TService> ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
            where TDecorator : class, TService;
        ICustomizesAutofacDecorator<TService> ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams);
    }

    public interface ICustomizesAutofacDecorator
    {
        ICustomizesAutofacDecorator ThenWrapWith<TDecorator>(params Parameter[] autofacParams)
            where TDecorator : class;
        ICustomizesAutofacDecorator ThenWrapWithType(Type decoratorType, params Parameter[] autofacParams);
    }
}