public interface ICustomizesDecorator<TService> where TService : class
{
    ICustomizesDecorator<TService> ThenWrapWith<TDecorator>()
        where TDecorator : TService;

    ICustomizesDecorator<TService> ThenWrapWithType(Type decoratorType);
}

public interface ICustomizesDecorator
{
    ICustomizesDecorator ThenWrapWith<TDecorator>()
        where TDecorator : class;

    ICustomizesDecorator ThenWrapWithType(Type decoratorType);
}