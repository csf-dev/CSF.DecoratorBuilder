using System;

namespace CSF.DecoratorBuilder
{
    public interface ICustomizesDecorator<in TService> where TService : class
    {
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator>(params TypedParam[] parameters)
            where TDecorator : class, TService;

        ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType, params TypedParam[] parameters);
    }

    public interface ICustomizesDecorator
    {
        ICustomizesDecorator ThenWrapWith<TDecorator>(params TypedParam[] parameters)
            where TDecorator : class;

        ICustomizesDecorator ThenWrapWithType(Type decoratorType, params TypedParam[] parameters);
    }
}