using System;

namespace CSF.DecoratorBuilder
{
    public interface ICreatesDecorator<in TService> where TService : class
    {
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>()
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1>(TParam1 param1)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2>(TParam1 param1, TParam2 param2)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
            where TInitialImpl : class, TService;
        ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
            where TInitialImpl : class, TService;

        ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1>(Type initialImplType, TParam1 param1);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2>(Type initialImplType, TParam1 param1, TParam2 param2);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2, TParam3>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2, TParam3, TParam4>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);
        ICustomizesDecorator<TService> UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }

    public interface ICreatesDecorator
    {
        ICustomizesDecorator UsingInitialImpl<TInitialImpl>()
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1>(TParam1 param1)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2>(TParam1 param1, TParam2 param2)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
            where TInitialImpl : class;
        ICustomizesDecorator UsingInitialImpl<TInitialImpl, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
            where TInitialImpl : class;

        ICustomizesDecorator UsingInitialImplType(Type initialImplType);
        ICustomizesDecorator UsingInitialImplType<TParam1>(Type initialImplType, TParam1 param1);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2>(Type initialImplType, TParam1 param1, TParam2 param2);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6);
        ICustomizesDecorator UsingInitialImplType<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>(Type initialImplType, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7);
    }
}