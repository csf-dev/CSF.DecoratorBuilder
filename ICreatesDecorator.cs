public interface ICreatesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> UsingInitialImpl<TInitialImpl>()
        where TInitialImpl : TService;

    ICustomizesDecorator<TService> UsingInitialImplType(Type initialImplType);
}

public interface ICreatesDecorator
{
    ICustomizesDecorator UsingInitialImpl<TInitialImpl>()
        where TInitialImpl : class;

    ICustomizesDecorator UsingInitialImplType(Type initialImplType);
}