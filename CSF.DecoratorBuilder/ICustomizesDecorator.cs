using System;

namespace CSF.DecoratorBuilder
{
    public interface ICustomizesDecorator<in TService> where TService : class
    {
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator>()
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1>(TParam1 param1)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2>(TParam1 param1, TParam2 param2)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
            where TDecorator : class, TService;
        ICustomizesDecorator<TService> ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
            where TDecorator : class, TService;

        ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1>(Type initialImplType, TParam1 param1);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2>(Type initialImplType, TParam1 param1, TParam2 param2);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2, TParam3>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2, TParam3, TParam4>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);
        ICustomizesDecorator<TService> ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }

    public interface ICustomizesDecorator
    {
        ICustomizesDecorator ThenWrapWith<TDecorator>()
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1>(TParam1 param1)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2>(TParam1 param1, TParam2 param2)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
            where TDecorator : class;
        ICustomizesDecorator ThenWrapWith<TDecorator, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
            where TDecorator : class;

        ICustomizesDecorator ThenWrapWithType(Type decoratorType);
        ICustomizesDecorator ThenWrapWithType<TParam1>(Type initialImplType, TParam1 param1);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2>(Type initialImplType, TParam1 param1, TParam2 param2);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);
        ICustomizesDecorator ThenWrapWithType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }
}