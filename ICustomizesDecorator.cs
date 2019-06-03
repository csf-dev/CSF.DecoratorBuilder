public interface ICustomizesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> ThenWrapWith<TDecorator>()
        where TDecorator : TService;
    // As ICreatesDecorator, this would also have 7 additional overloads for accepting typed parameters

    ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType);
    // As ICreatesDecorator, this would also have 7 additional overloads for accepting typed parameters

    ICustomizesDecorator<TService> WithGlobalParameters<TParam1>(TParam1 param1);
    // As ICreatesDecorator, this would also have 7 overloads for accepting typed parameters
}

public interface ICustomizesDecorator
{
    ICustomizesDecorator ThenWrapWith<TDecorator>()
        where TDecorator : class;
    // As ICreatesDecorator, this would also have 7 additional overloads for accepting typed parameters

    ICustomizesDecorator ThenWrapWithType(Type decoratorType);
    // As ICreatesDecorator, this would also have 7 additional overloads for accepting typed parameters

    ICustomizesDecorator WithGlobalParameters<TParam1>(TParam1 param1);
    // As ICreatesDecorator, this would also have 7 overloads for accepting typed parameters
}