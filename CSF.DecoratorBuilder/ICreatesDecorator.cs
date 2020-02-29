using System;

namespace CSF.DecoratorBuilder
{
    public interface ICreatesDecorator<in TService> where TService : class
    {
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>(params TypedParam[] parameters)
            where TInitialImpl : class, TService;

        ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType, params TypedParam[] parameters);
    }

    public interface ICreatesDecorator
    {
        ICustomizesDecorator UsingInitialImpl<TInitialImpl>(params TypedParam[] parameters)
            where TInitialImpl : class;

        ICustomizesDecorator UsingInitialImplType(Type initialImplType, params TypedParam[] parameters);
    }
}