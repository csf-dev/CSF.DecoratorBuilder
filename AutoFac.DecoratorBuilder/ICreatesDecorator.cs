public interface ICreatesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>()
        where TInitialImpl : TService;
    ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl,TParam1>(TParam1 param1)
        where TInitialImpl : TService;
    ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl,TParam1,TParam2>(TParam1 param1, TParam2 param2)
        where TInitialImpl : TService;
    // ... and so on up to TParam7 for 7 parameters

    ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType);
    // The above would also have 7 additional overloads for accepting typed parameters
}

public interface ICreatesDecorator
{
    ICustomizesDecorator UsingInitialImpl<TInitialImpl>()
        where TInitialImpl : class;
    // As above, this would also have 7 additional overloads for accepting typed parameters

    ICustomizesDecorator UsingInitialImplType(Type initialImplType);
    // As above, this would also have 7 additional overloads for accepting typed parameters
}